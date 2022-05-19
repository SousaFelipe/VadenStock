﻿using System;



namespace VadenStock.Model.Types
{
    public struct CategoriaType : IModelType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
