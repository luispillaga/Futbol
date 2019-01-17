using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class ConfiguracionSingleton
    {
        private static ConfiguracionSingleton singleton = null;
        public Configuracion configuracion { get; set; }

        public ConfiguracionSingleton()
        {
            configuracion = new Configuracion();
        }

        public static ConfiguracionSingleton GetInstance()
        {
            if (singleton == null)
            {
                singleton = new ConfiguracionSingleton();
            }
            return singleton;
        }
    }
}