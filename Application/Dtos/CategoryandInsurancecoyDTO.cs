﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CategoryandInsurancecoyDto
    {
       
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CoyId { get; set; }
        public string? CoyName { get; set; }
        public InsuranceCoyDetailDto InsuranceCoy { get; set; }
        public CreateCategoryDto CreateCategory { get; set; }
    }
}
