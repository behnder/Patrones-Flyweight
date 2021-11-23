using System;
using System.Collections.Generic;

namespace Patrones_Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            //Se generan 10 millones de proyectiles 
            List<Proyectil> proyectiles = new List<Proyectil>();
            for (int i = 0; i < 10000000; i++)
            {
                Proyectil proyectil = new Proyectil("Proyectil01");
                proyectiles.Add(proyectil);

            }
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
   

    #region Sin FlyWeight
    //USO DE MEMORIA 3.5GB
    //class Proyectil
    //{
    //    //propiedades unicas
    //    string id;
    //    string posicion;

    //    //propiedades compartidas
    //    string dibujarModelo3D;
    //    string colorear;
    //    string dibujarParticula;


    //    public Proyectil(string tipoProyectil)
    //    {

    //        this.id = DateTime.Now.GetHashCode().ToString("x");
    //        Random random = new Random();
    //        this.posicion = "X: " + random.Next(0, 100) + " Y: " + random.Next(0, 100);

    //        this.dibujarModelo3D = tipoProyectil;
    //        this.colorear = tipoProyectil +" gris";
    //        this.dibujarParticula = tipoProyectil +" particula amarilla";

    //    }


    //}
    #endregion

    #region Con FlyWeight
    //USO DE MEMORIA 1.7GB
    static class FlyweightFactory
    {
        private static Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();

        public static Flyweight GetFlyweight(string tipoProyectil)
        {
            if (flyweights.ContainsKey(tipoProyectil))
            {
                return flyweights[tipoProyectil];
            }
            else
            {
                flyweights.Add(tipoProyectil, new ProyectilPropiedadCompartida(tipoProyectil));
                return flyweights[tipoProyectil];
            }
        }

    }

    interface Flyweight
    {

    }
    class ProyectilPropiedadCompartida : Flyweight
    {
        string dibujarModelo3D;
        string colorear;
        string dibujarParticula;


        public ProyectilPropiedadCompartida(string tipoProyectil)
        {

            this.dibujarModelo3D = tipoProyectil;
            this.colorear = tipoProyectil + " gris";
            this.dibujarParticula = tipoProyectil + " particula amarilla";

        }

        public override string ToString()
        {
            return $"{this.dibujarModelo3D} {this.colorear} {this.dibujarParticula}";
        }
    }

    class Proyectil
    {

        string id;
        string posicion;

        private Flyweight Flyweight;


        public Proyectil(string tipoProyectil)
        {
            Flyweight = FlyweightFactory.GetFlyweight(tipoProyectil);
            this.id = DateTime.Now.GetHashCode().ToString("x");
            Random random = new Random();
            this.posicion = "X: " + random.Next(0, 100) + " Y: " + random.Next(0, 100);
        }

        public override string ToString()
        {
            return $"{this.id} {this.posicion} {Flyweight.ToString()}";
        }
    }

    #endregion

}
