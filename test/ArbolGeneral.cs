using System;
using System.Collections.Generic;

namespace DeepSpace
{
	public class ArbolGeneral<T>
	{
		
		private T dato;
		private List<ArbolGeneral<T>> hijos = new List<ArbolGeneral<T>>();

		public ArbolGeneral(T dato) {
			this.dato = dato;
		}
	
		public T getDatoRaiz() {
			return this.dato;
		}
	
		public List<ArbolGeneral<T>> getHijos() {
			return hijos;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.getHijos().Add(hijo);
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.getHijos().Remove(hijo);
		}
	
		public bool esHoja() {
			return this.getHijos().Count == 0;
		}
		
		public bool esVacio() {
			return this.dato == null;
		}
	
		public int altura()
		{
			if (this.esHoja())
				return 0;
			else
			{
				int alturaMaxima=-1;
				foreach(var element in this.getHijos())
				{
					int alturaHijo= element.altura();
					
					if (alturaHijo>alturaMaxima)
					{
						alturaMaxima=alturaHijo;
					}
					
				}
				
				return alturaMaxima + 1;			
			}
		}
		
		public Cola<ArbolGeneral<T>> porNivelesConSeparacion(){
			
			Cola<ArbolGeneral<T>> cola = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux;
			
			//encolamos raiz
			cola.encolar(this);
			
			//procesamos cola
			while(!cola.esVacia()){
				arbolAux = cola.desencolar();
				
				//procesar el dato
				Console.Write(arbolAux.getDatoRaiz() + " ");
				
				//encolamos hijos
				foreach(var hijo in arbolAux.getHijos())
					cola.encolar(hijo);
				
			}
			return cola;
		}
	
	
	}
}