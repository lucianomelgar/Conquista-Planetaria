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

			//creo una lista y genero la recursion, este metodo me devolvera la lista de los desencientes de la IA
			List<string> listaRecorrida = recorrerArbol(planetaDeIA);
			
			//creo una variable string para almacenar los datos de cada planeta en forma de string
			string listaNodos = "";
			
			//con el foreach recorro cada elemento de listaRecorrida, la lista que retornada donde se genero la recursión
			foreach(var elemento in listaRecorrida){
				listaNodos += elemento.ToString() + " , ";	//por cada elemento de la lista se agregara a la variable listaNodos, generando un string con todos los elementos
			}
			
			return "2) Descendientes de la IA: " + listaNodos.ToString();	
		}
		
		
		//recorre un arbol que le pasen por parametro
		private List<string> recorrerArbol (ArbolGeneral<Planeta> arbol){
			
			//creo lista para almacenar nodos
			List<string> listado = new List<string>();
			
			//proceso primero la raiz
			listado.Add(arbol.getDatoRaiz().population.ToString());
			
			//visito cada hijo del nodo actual
			foreach(var subArbol in arbol.getHijos()){
				
				listado.AddRange(recorrerArbol(subArbol));
			}
			return listado;
		}
		

		//retorna el nodo de la IA	
		private ArbolGeneral<Planeta> planetaIA (ArbolGeneral<Planeta> arbol){
			
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
		
		
		// Calcula y retorna en un texto la población total y promedio por cada nivel del árbol.
		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{	
			//POBLACION TOTAL
			List<Planeta> camino = new List<Planeta>();
			
			long contador = 0;
			
			long contadorTotal = this._Consulta3(camino, arbol, contador);
			
			
			//PROMEDIO POR CADA NIVEL DEL ÁRBOL
			Cola<ArbolGeneral<Planeta>> niveles = arbol.porNivelesConSeparacion();
			
			
			
			
			return "3) Total de naves: " + contadorTotal.ToString() + "\n" + niveles;
			
			
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
