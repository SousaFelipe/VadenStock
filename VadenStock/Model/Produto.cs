﻿using System;

using MySql.Data.MySqlClient;



namespace VadenStock.Model
{
    public class Produto : Connection
    {
        public struct Contract
        {
            public int Id { get; set; }
            public Tipo.Contract Tipo { get; set; }
            public Marca.Contract Marca { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public decimal Price { get; set; }
            public DateTime CreatedDate { get; set; }
        }



        public Contract? Load(int id)
        {
            try
            {
                using (Plug = new MySqlConnection(ConnectionString))
                {
                    Plug.Open();

                    using (Cmmd = new MySqlCommand(RawLoad("produtos", id), Plug))
                    {
                        using (Reader = Cmmd.ExecuteReader())
                        {
                            if (Reader.Read())
                                return Content.Get(Reader);
                        }
                    }
                }
            }
            finally
            {
                Unplug();
            }

            return null;
        }

         

        private class Content
        {
            public static Contract Get(MySqlDataReader reader)
            {
                #pragma warning disable CS8629
                Contract contract = new()
                {
                    Id = reader.GetInt32("id"),
                    Tipo = (Tipo.Contract)new Tipo().Load(reader.GetInt32("tipo")),
                    Marca = (Marca.Contract)new Marca().Load(reader.GetInt32("marca")),
                    Name = reader.GetString("name"),
                    Code = reader.GetString("code"),
                    Price = reader.GetDecimal("price"),
                    CreatedDate = reader.GetDateTime("created_at")
                };

                return contract;
            }
        }
    }
}
