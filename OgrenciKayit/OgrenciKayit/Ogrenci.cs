using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciKayit
{
    public class Ogrenci
    {
        #region Constructor
        public Ogrenci()
        {
            KayitTarihi = DateTime.Now;
            ID = Guid.NewGuid();
        }
        public Ogrenci(string ad, string soyad, DateTime dogumTarihi, bool erkekMi)
        {
            Ad = ad;
            Soyad = soyad;
            DogumTarihi = dogumTarihi;
            ErkekMi = erkekMi;
            KayitTarihi = DateTime.Now;
            ID = Guid.NewGuid();
        }
        #endregion
        #region Fields
        private Guid _id;
        private string _ad;
        private string _soyad;
        private DateTime _dogumTarihi;
        private bool _erkekMi;
        private DateTime _kayitTarihi;
        #endregion
        #region Properties
        public Guid ID
        {
            private set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        public string Ad
        {
            get
            {
                return _ad;
            }
            set
            {
                KarakterKontrol(value);
                string basHarfBuyuk = AdBuyukKucukHarf(value);
                _ad = basHarfBuyuk;
            }
        }
        public string Soyad
        {
            get
            {
                return _soyad.ToUpper();
            }
            set
            {
                KarakterKontrol(value);
                _soyad = AdBuyukKucukHarf(value);
            }
        }
        public DateTime DogumTarihi
        {
            get
            {
                return _dogumTarihi;
            }
            set
            {
                TimeSpan aralık = DateTime.Now - value;
                if (aralık.TotalDays < 1980)
                    throw new Exception("5.5 yaşından küçük bebeleri kaydetmiyoruz");
                _dogumTarihi = value;
            }
        }
        public bool ErkekMi
        {
            get
            {
                return _erkekMi;
            }
            set
            {
                _erkekMi = value;
            }
        }
        public int Yas
        {
            get
            {
                TimeSpan aralık = DateTime.Now - _dogumTarihi;
                return Convert.ToInt32(Math.Ceiling(aralık.TotalDays / 365));
            }
        }
        public DateTime KayitTarihi
        {
            private set
            {
                _kayitTarihi = value;
            }
            get
            {
                return _kayitTarihi;
            }
        }
        #endregion
        #region Methods
        private string AdBuyukKucukHarf(string isim) => isim.Substring(0, 1).ToUpper() + isim.Substring(1).ToLower();

        private void KarakterKontrol(string kelime)
        {
            foreach (char item in kelime)
            {
                if (char.IsDigit(item))
                    throw new Exception("İsminizde rakam bulunamaz!");
                else if (char.IsSymbol(item) || char.IsPunctuation(item))
                    throw new Exception("İsminizde özel karakter bulunamaz");
            }
        }
        #endregion
        #region Override Methods
        public override string ToString() => $"{Ad} {Soyad} - {Yas}";

        #endregion
    }
}
