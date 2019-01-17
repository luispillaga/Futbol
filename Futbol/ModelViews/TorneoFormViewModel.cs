using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Futbol.ModelViews
{
	public class TorneoFormViewModel
	{
		public int torneo_id { get; set; }

		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(100)]
		[DataType(DataType.Text)]
		public string torneo_nombre { get; set; }

		[Display(Name = "Fecha Inicio")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[DataType(DataType.Date)]
		public System.DateTime torneo_fecha_inicio { get; set; }

		[DataType(DataType.Time)]
		public System.TimeSpan torneo_hora_inicio { get; set; }

		[Display(Name = "Estado")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(20)]
		[DataType(DataType.Text)]
		public string torneo_estado { get; set; }

		[Display(Name = "Precio")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[DataType(DataType.Currency)]
		public decimal torneo_precio { get; set; }

		[Display(Name = "Descripción")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(200)]
		[DataType(DataType.MultilineText)]
		public string torneo_descripcion { get; set; }

		public Direccion Direccion { get; set; }
		public Imagen Imagen { get; set; }
		public int provincia_id { get; set; }
		public int ciudad_id { get; set; }
	}
}