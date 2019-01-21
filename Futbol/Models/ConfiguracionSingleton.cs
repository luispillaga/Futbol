using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Futbol.Models
{
    public class ConfiguracionSingleton
    {
        private static ConfiguracionSingleton singleton = null;
        public Configuracion configuracion { get; set; }

        public ConfiguracionSingleton()
        {
            configuracion = new Configuracion();
            configuracion.EstadosTorneo = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Configurando",
                    Value = "Configurando"
                },
                new SelectListItem()
                {
                    Text = "En Progreso",
                    Value = "En Progreso"
                },
                new SelectListItem()
                {
                    Text = "Finalizado",
                    Value = "Finalizado"
                }
            };
            configuracion.EstadosEquipo = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Sin Confirmar",
                    Value = "Sin Confirmar"
                },
                new SelectListItem()
                {
                    Text = "Confirmado",
                    Value = "Confirmado"
                },
                new SelectListItem()
                {
                    Text = "Sancionado",
                    Value = "Sancionado"
                }
            };
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