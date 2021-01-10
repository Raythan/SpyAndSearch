﻿using System.Linq;
using System.Windows.Forms;
using WebScrapperLib;
using WebScrapperLib.ScrapperController;

namespace LookAndSearchInterface
{
    public partial class WorldsForm : Form
    {
        WorldScrapper ScrapperService = new WorldScrapper();
        public WorldsForm()
        {
            InitializeComponent();
        }

        public void LoadComponentsData()
        {
            cboBoxWorldNames.DataSource = null;
            cboBoxWorldNames.DataSource = ScrapperService.DictionaryEntity.Keys.ToList();
        }

        private void cboBoxWorldNames_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboBoxWorldNames.Text))
            {
                dynamic entity = new WebScrapperLib.Models.WorldEntity();
                ScrapperService.DictionaryEntity.TryGetValue(cboBoxWorldNames.Text, out entity);
                txtBoxOnline.Text = entity.Online.ToString();
                txtBoxLocation.Text = entity.Location;
                txtBoxPvpType.Text = entity.PvpType;
                txtBoxAdditionalInformation.Text = entity.AdditionalInformation;

                if (!string.IsNullOrEmpty(entity.BattleEye) && !entity.BattleEye.Equals("nothing"))
                {
                    picBoxBattleEye.BorderStyle = BorderStyle.None;
                    picBoxBattleEye.Image = Extender.RecoverImageFromUrl(entity.BattleEye, "IconSize18x18", "CipSoftHeaders");
                }
                else
                {
                    picBoxBattleEye.BorderStyle = BorderStyle.FixedSingle;
                    picBoxBattleEye.Image = null;
                }
            }
        }
        
        private void btnRefreshWorldDictionary_Click(object sender, System.EventArgs e)
        {
            lblHoraUltimaAtualizacao.Text = $"Last time updated: || Refreshing... ||";
            ScrapperService.RecoverScrapperData();
            LoadComponentsData();
            lblHoraUltimaAtualizacao.Text = $"Last time updated: {ScrapperService.LastUpdateEntity.ToString(Extender.DateTimeFormatBrazil)}";
        }
    }
}
