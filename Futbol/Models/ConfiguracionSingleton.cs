﻿using System;
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
            configuracion.EstadosJugador = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Validado",
                    Value = "Validado"
                },
                new SelectListItem()
                {
                    Text = "Suspendido",
                    Value = "Suspendido"
                },
                new SelectListItem()
                {
                    Text = "Retirado",
                    Value = "Retirado"
                }
            };
            configuracion.EstadosLlave = new List<SelectListItem>()
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
            configuracion.NombreGrupoDefault = new string[]
            {
                "Grupo A",
                "Grupo B",
                "Grupo C",
                "Grupo D",
                "Grupo F",
                "Grupo G",
                "Grupo H",
                "Grupo I",
                "Grupo J",
                "Grupo K",
                "Grupo L",
                "Grupo M",
                "Grupo N",
                "Grupo O",
                "Grupo P",
                "Grupo R",
                "Grupo S",
                "Grupo T",
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