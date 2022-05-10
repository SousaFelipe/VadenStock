﻿using System.Collections.Generic;

using VadenStock.Model;
using VadenStock.Model.Types;

using VadenStock.View.Structs;



namespace VadenStock.View.Models
{
    public static class ItensViewModel
    {
        public static List<ItemType> TodosOsItens
        {
            get { return Item.New.Select().Get(); }
        }



        public static List<ItemType> ItensPorProduto(int produto)
        {
            return Item.New
                .Select()
                .Where("produto", produto.ToString())
                .Get();
        }

        public static int CountItensPorProduto(int produto)
        {
            return Item.New
                .Count()
                .Where("produto", produto.ToString())
                .Bind();
        }

        public static List<ItemType> ItensPorProdutoByStatus(int produto, string status)
        {
            return Item.New
                .Select()
                .Where("produto", produto.ToString())
                .Where("localizacao", status)
                .Get();
        }



        public static int CountItensPorAlmoxarifado(int almoxarifado)
        {
            return Item.New
                .Count()
                .Where("almoxarifado", almoxarifado.ToString())
                .Bind();
        }

        public static List<ItemType> ItensPorAlmoxarifado(int almoxarifado)
        {
            return Item.New
                .Select()
                .Where("almoxarifado", almoxarifado.ToString())
                .Get();
        }
    }
}
