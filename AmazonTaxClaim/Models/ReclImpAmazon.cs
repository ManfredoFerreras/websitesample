using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonTaxClaim.Models
{
    [Table("RECL_IMP_AMAZON")]
    public class ReclImpAmazon
    {

        [Key]
        [Display(Name ="Id")]
        public int CLAIM_ID { set; get; }

        [Required(ErrorMessage ="*")]
        [Display(Name = "EPS")]
        public String CTE_NUMERO_EPS { set; get; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Fecha")]
        public DateTime FECHA { set; get; }
   
        [Required(ErrorMessage = "*")]
        [Display(Name = "Estado")]
        public int ESTADO { set; get; }

        [Required]
        [Display(Name = "Archivo")]
        public String ARCHIVO { set; get; }

        [Required]
        [Display(Name = "Ruta")]
        public String RUTA { set; get; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Período")]
        public int  PERIODO { set; get; }


    }

    public class dbepsContext : DbContext
    {
        public DbSet<ReclImpAmazon> Reclamos { get; set; }
    }
}