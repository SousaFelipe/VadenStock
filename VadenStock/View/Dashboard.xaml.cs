﻿using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using VadenStock.View.Models;
using VadenStock.View.Structs;
using VadenStock.View.Components.Cards;

using VadenStock.Model.Types;



namespace VadenStock.View
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            Loaded += delegate {
                LoadCardPatrimonio();
                LoadCardEstoqueMinMax();
                LoadAlmoxCards();
            };
        }



        public void LoadCardPatrimonio()
        {
            PatrimonioStruct patrimonio = DashboardViewModel.GetPatrimonios();

            _PatrimonioEmEstoque.UpdateValue(patrimonio.Estoque, patrimonio.Total);
            _PatrimonioEmComodato.UpdateValue(patrimonio.Comodato, patrimonio.Total);
            _PatrimonioEmProducao.UpdateValue(patrimonio.Producao, patrimonio.Total);
        }



        private void LoadCardEstoqueMinMax()
        {
            List<CategoriaType> categorias = DashboardViewModel.GetCategorias();

            for (int i = 0; i < categorias.Count; i++)
            {
                _ComboCategorias.Items.Add(new ComboBoxItem()
                {
                    Tag = categorias[i].Id,
                    Content = string.Concat(categorias[i].Name[0], categorias[i].Name[1..].ToLower())
                });
            }

            _ColumnChartMinMax.SetSeries(new double[] { 45, 23, 84, 19, 61, 42, 77 });
            _ColumnChartMinMax.SetLabels(new string[] { "MKT", "UBQ", "ITB", "HWY", "SAE", "OPB", "LNA" });
            _ColumnChartMinMax.Draw();
        }



        private void LoadAlmoxCards()
        {
            List<AlmoxType> almoxarifados = DashboardViewModel.GetAlmoxarifados();

            _GridAlmoxarifados.RowDefinitions.Add(new RowDefinition());

            if (almoxarifados != null)
            {
                AlmoxType currentAlmo;
                ThumbnailCard currentCard;

                int r = 0;
                int l = 0;

                for (int a = 0; a < almoxarifados.Count; a++)
                {
                    currentAlmo = almoxarifados[a];
                    currentCard = Molecules.AlmoxarifadoThumbCard(currentAlmo, a, r);

                    _GridAlmoxarifados.Children.Add(currentCard);
                     Grid.SetColumn(currentCard, r);
                     Grid.SetRow(currentCard, l);

                    if (0 == (r - 3))
                    {
                        _GridAlmoxarifados.RowDefinitions.Add(new RowDefinition());

                        r = 0;
                        l++;
                    }
                    else r++;
                }
            }
        }



        private void ComboCategorias_Update(object sender, SelectionChangedEventArgs e)
        {
            object tag = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag;
            int tagNum = int.Parse(tag.ToString());

            if (_ComboTipos != null)
            {
                var tipos = DashboardViewModel.GetTipos(tagNum);

                _ComboTipos.Clear(true);

                for (int i = 0; i < tipos.Count; i++)
                {
                    _ComboTipos.Items.Add(new ComboBoxItem()
                    {
                        Tag = tipos[i].Id,
                        Content = string.Concat(tipos[i].Name[0], tipos[i].Name[1..].ToLower())
                    });
                }
            }
        }



        private static class Molecules
        {
            public static ThumbnailCard AlmoxarifadoThumbCard(AlmoxType almox, int position, int rounds)
            {
                Thickness thickn = new(6, (position > 0 && ((rounds - 3) == -3)) ? 12 : 0, 6, 0);
                string subHeader = Utils.ZeroFill(DashboardViewModel.GetItems(almox.Id).Count) + " itens disponíveis";

                ThumbnailCard thumb = new()
                {
                    Margin = thickn,
                    Body = "#FFFFFF",
                    Header = almox.Name,
                    SubHeader = subHeader
                };

                thumb.SetThumb(
                        almox.Tipo == AlmoxType.Hosted.Carro ? "car" : almox.Tipo == AlmoxType.Hosted.Moto ? "bike" : "warehouse"
                    );

                return thumb;
            }
        }
    }
}
