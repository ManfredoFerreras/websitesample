using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.BO.Models
{
	public class Clientes
	{
		[Key]
		public string CTE_NUMERO_EPS { set; get; }

		public string CTE_CEDULA { set; get; }

		public string CTE_RNC { set; get; }

		public string CTE_NOMBRE { set; get; }

		public string CTE_APELLIDO { set; get; }

		public string CTE_DIRECCION_CASA { set; get; }

		public string CTE_DIRECCION_OFICINA { set; get; }

		public string CTE_CIUDAD { set; get; }

		public string CTE_TELEFONO_CASA { set; get; }

		public string CTE_TELEFONO_OFICINA { set; get; }

		public DateTime? CTE_FECHA_NACIMIENTO{ set; get; }

		[Required]
		public string CTE_TIPO { set; get; }

		public string RES_CODIGO { set; get; }

		[Required]
		public string CTE_ESTADO { set; get; }

		public string CTE_REPRESENTANTE { set; get; }

		[Required]
		public string CTE_CREDITO { set; get; }

		public string CTE_CODIGO_VOICE { set; get; }

		public string AGE_CODIGO { set; get; }

		public string SUC_CODIGO { set; get; }

		[Display(Name = "Email")]
		public string CTE_EMAIL { set; get; }

		[Display(Name = "Tarifa")]
		[Required]
		public string TAR_CODIGO { set; get; }

		public string GRC_CODIGO { set; get; }

		public Decimal CTE_LIMITE_CREDITO { set; get; }

		public int CTE_DIAS_CREDITOS { set; get; }

		public Decimal CTE_CREDITO_DISPONIBLE { set; get; }

		public Decimal CTE_BALANCE_DISPONIBLE { set; get; }

        public Decimal CTE_DIA_CORTE { set; get; }

		[Display(Name = "Celular")]
		public string CTE_CELULAR { set; get; }

		[Required]
		[MaxLength(1)]
		[Display(Name = "Correspondencia")]
		public string CTE_CORRESPONDENCIA { set; get; }

		[Required]
		public int COM_CODIGO { set; get; }

		[Required]
		[MaxLength(1)]
		[Display(Name ="Tipo Fiscal")]
		public string STC_CODIGO { set; get; }


        public ICollection<MyUserClaim> Claims { get; set; }


    }

    public class MyUserClaim
    {
        public MyUserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
