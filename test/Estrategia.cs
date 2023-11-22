
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		
		//retorna Distancia del camino que existe entre el planeta del Bot y la raíz:
		public String Consulta1(ArbolGeneral<Planeta> arbol){
			
			List<Planeta> caminoHastaIA = new List<Planeta>();
			
			this._calcularMovimiento1(caminoHastaIA, arbol);
			
			int caminoBotRaiz = caminoHastaIA.Count - 1;
			
			return ("1) Distancia del camino que existe entre el planeta del Bot y la raíz: " + caminoBotRaiz.ToString());
		}
	
		
		
		//Retorna un listado de nodos descendientes de la IA
		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			
			//se crea una variable planeta a la cual se le asigna el metodo que retorna el planeta de la IA		
			ArbolGeneral<Planeta> planetaDeIA = this.planetaIA(arbol);
			
			//se crea una lista donde se alvergaran los hijos de la IA
			List<Planeta> hijos = new List<Planeta>();
			

			//CON EL FOREACH SOLO RECORREMOS LOS HIJOS MÁS CERCANOS, NO TODOS LOS DESCENCIENTES
			//SE DEBE RECORRER RECURSIVAMENTE
			foreach(var nodoHijo in planetaDeIA.getHijos()){
				
				//creo lista para almacenar la población de cada nodo hijo
				List<Planeta> listaAux = new List<Planeta>();
				
				hijos.Add(nodoHijo.getDatoRaiz());
				
				listaAux.Add(nodoHijo.getDatoRaiz());
				
			}
			
			foreach(var elemento in hijos){
				
					
				Console.WriteLine(elemento.population.ToString());
				return "Hijos de la IA";
			}
			
			return "2) IA: ";	
		}
		
		

		private List<Planeta> descendienteDeNodo(List<Planeta> listadoDeDescendientes, ArbolGeneral<Planeta> arbol){
			
			//caso base, si el nodo es hoja, agregar el nodo a la lista y retornarla
			if(arbol.esHoja()){
				listadoDeDescendientes.Add(arbol.getDatoRaiz());
				
				return listadoDeDescendientes;
			}
			
			else{
				//creo lista auxiliar para albergar listado de nodos descendientes
				List<Planeta> listadoAuxiliar = new List<Planeta>();
				
				//creo arbol auxiliar y genero la recursón
				ArbolGeneral<Planeta> planetaAux = this.descendienteDeNodo(listadoAuxiliar, arbol);
				
				//chequear si con cada recursion se crea una nueva lista o funciona
				//quedaria hacer la comparación de las listas y quedarse con la que contenga los nodos
				
				
				
			}
		}
			
		
		public List<Planeta> ObtenerNodosHijosRecursivo(ArbolGeneral<Planeta> arbol){
		
		    List<Planeta> listaNodosHijos = new List<Planeta>();
		
		    foreach (var nodo in arbol.getHijos())
		    {
		    	listaNodosHijos.Add(nodo.getDatoRaiz());
		        listaNodosHijos.AddRange(ObtenerNodosHijosRecursivo(nodo));
		    }
		
		    return listaNodosHijos;
		}
		
			
		public ArbolGeneral<Planeta> planetaIA (ArbolGeneral<Planeta> arbol){
			
			//si el nodo evaluado es de la IA, lo retorna, caso base
			if (arbol.getDatoRaiz().EsPlanetaDeLaIA())
				return arbol;
			
			else{
				foreach(var elemento in arbol.getHijos()){
					
					ArbolGeneral<Planeta> elementoNodo = planetaIA(elemento);
					
					//meter condicional de SI es Hoja
					if(elementoNodo != null)
						return elementoNodo;
				}
				return null;
				
			}
			
		}
		//CHEQUEAR SI SE PUEDE CON EL ULTIMO ELEMENTO DE LA LISTA DE CAMINOHASTAIA
		
		public void listadoNodos (List<Planeta> camino, ArbolGeneral<Planeta> arbol){
			this._calcularMovimiento1(camino, arbol);
			
			foreach(var elem in camino){
				Console.Write(elem.population + ", ");
				
			}
			
		}
		
	

		//retorna contador con la suma de la poblacion de todos los planetas
		private long _Consulta3(List<Planeta> camino, ArbolGeneral<Planeta> arbol, long contador){
			
			camino.Add(arbol.getDatoRaiz());
			
			contador += arbol.getDatoRaiz().population;
			
			foreach(var hijo in arbol.getHijos()){
				long contAux = this._Consulta3(camino, hijo, contador);
					if (contador < contAux)
						contador = contAux ;
			}
			return contador;	
		}
		
		
		//retorna el número de planetas con población mayor a la cantidad promedio del árbol
		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{	
			
			return arbol.porNivelesConSeparacion().ToString();
			
		}

		
		
		
		//retorna lista de hijos de un planeta
		private List<Planeta> buscarHijos(List<Planeta> hijos, ArbolGeneral<Planeta> arbol){
			
			if(arbol.esHoja()){						//si arbol es hoja
				hijos.Add(arbol.getDatoRaiz());		//agrega ese planeta del arbol a la lista hijos
				return hijos;						//retorna lista hijos. Caso base
			}
			else{														//sino es hoja
				foreach(var subArbol in arbol.getHijos()){					//por cada variable subarbol en Arbol.gethijos()
																				
					if(!arbol.getDatoRaiz().Equals(hijos[hijos.Count-1])){	  //si el planeta del arbol NO es igual a el ultimo elemento de la lista hijos
																			  
						hijos.Add(arbol.getDatoRaiz());						  		//agregar ese planeta del arbol a la lista hijos
					}															
					List<Planeta> hijosAux = this.buscarHijos(hijos, subArbol);
																			  //creo lista hijosAux y genero la recursion para entrar a los hijos del arbol

					return hijosAux;										  //retorno la lista de hijos del hijo del arbol procesado
				}													
			}														
			return hijos;										//se retorna la lista obtenida		
		}

		

		
		//retorna un camino desde la raiz hasta el nodo de la IA
		private List<Planeta> _calcularIA(List<Planeta> camino,ArbolGeneral<Planeta> arbol)
		{
			//primero proceso la raiz del arbol, agregandola a una lista
			camino.Add(arbol.getDatoRaiz());
			
			//si encontramos planeta
			if(arbol.getDatoRaiz().EsPlanetaDeLaIA())
				return camino;		//retornamos camino encontrado, primer iteracion caso base
			
			else{
				//luego se procesan hijos recursivamente
				foreach(var hijo in arbol.getHijos()){
					List<Planeta> caminoAux = _calcularIA(camino, hijo);
					
					if(caminoAux != null)
						return caminoAux;
				}
				//si el camino recorrida no encuentra un nodo de la IA se descartan nodos inutiles
				camino.RemoveAt(camino.Count - 1);		
			}
			return null;
		}
		
		
		//retorna camino desde la raiz hasta planeta del Jugador
		private List<Planeta> _calcularJugador(List<Planeta> camino, ArbolGeneral<Planeta> arbol)
		{
			//primero proceso la raiz del arbol, agregandola a una lista
			camino.Add(arbol.getDatoRaiz());
			
			//si encontramos planeta
			if(arbol.getDatoRaiz().EsPlanetaDelJugador()){
				
				return camino;		//retornamos camino encontrado, primer iteracion caso base
			}
			else{
				//luego se procesan hijos recursivamente
				foreach(var hijo in arbol.getHijos()){
					List<Planeta> caminoAux = _calcularJugador(camino, hijo);	//caminoAux retornara una lista desde la raiz hasta el Jugador
					
					if(caminoAux != null)			//si caminoAux es distinto de null
						return caminoAux;			//se retornara el camino con dicho listado
				}
				//si el camino recorrida no encuentra un nodo del Jugador se descartan nodos inutiles
				camino.RemoveAt(camino.Count - 1);
				
			}
			return null;
		}
		
		
		
		//retorna una lista con un camino desde la raiz hasta el planeta de la IA
		private List<Planeta> _calcularMovimiento1(List<Planeta> camino, ArbolGeneral<Planeta> arbol)
		{
			//primero proceso la raiz del arbol, agregandola a una lista
			camino.Add(arbol.getDatoRaiz());
			
			//si encontramos planeta
			if(arbol.getDatoRaiz().EsPlanetaDeLaIA())
				return camino;		//retornamos camino encontrado, primer iteracion caso base
			
			else{
				//luego se procesan hijos recursivamente
				foreach(var hijo in arbol.getHijos()){
					List<Planeta> caminoAux = _calcularMovimiento1(camino, hijo);
					
					if(caminoAux != null)
						return caminoAux;
				}
				//si el camino recorrida no encuentra un nodo de la IA se descartan nodos inutiles
				camino.RemoveAt(camino.Count - 1);		
			}
			return null;
			
		}
		

		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol){
			
			List<Planeta> caminoHastaIA = new List<Planeta>();
			
			List<Planeta> caminoHastaJugador = new List<Planeta>();
			
			this._calcularMovimiento1(caminoHastaIA, arbol);
			
			this._calcularJugador(caminoHastaJugador ,arbol);
				
			//recorrido de la lista hasta IA
			foreach(var nodo in caminoHastaIA){
				if (nodo.EsPlanetaDeLaIA()){
					
					if (caminoHastaIA.IndexOf(nodo) != 0){
						
						Movimiento movHaciaRaiz = new Movimiento (caminoHastaIA[caminoHastaIA.IndexOf(nodo)] , caminoHastaIA[caminoHastaIA.IndexOf(nodo)-1]);
						
						return movHaciaRaiz;
					}
				}
			}
			
			//recorrido de la lista hasta jugador
			foreach (var nodo in caminoHastaJugador){
				
				if (caminoHastaJugador[caminoHastaJugador.IndexOf(nodo)+1].EsPlanetaDelJugador()){
					
					Movimiento movHaciaJugador = new Movimiento(caminoHastaJugador[caminoHastaJugador.IndexOf(nodo)] , caminoHastaJugador[caminoHastaJugador.IndexOf(nodo)+1]);
					
					return movHaciaJugador;
				}
				
				if (caminoHastaJugador[caminoHastaJugador.IndexOf(nodo)+1].EsPlanetaNeutral()){
					
					Movimiento movHaciaJugador = new Movimiento(caminoHastaJugador[caminoHastaJugador.IndexOf(nodo)] , caminoHastaJugador[caminoHastaJugador.IndexOf(nodo)+1]);
					
					return movHaciaJugador;	
				}		
			}

			return null;			
		}	
				
	}
}
