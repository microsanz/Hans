using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hans
{
    class ItemModel
    {
        private String kodeProduk;
        private String jumlahProduk;
        private String hargaPerProduk;
        private String totalHarga;

        public ItemModel(string kodeProduk, string jumlahProduk, string hargaPerProduk, string totalHarga)
        {
            this.kodeProduk = kodeProduk;
            this.jumlahProduk = jumlahProduk;
            this.hargaPerProduk = hargaPerProduk;
            this.totalHarga = totalHarga;
        }
    }
}
