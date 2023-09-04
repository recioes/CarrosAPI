using CsvHelper.Configuration;

namespace CarrosAPI.Models
{
    public sealed class CarrosModelClassMap : ClassMap<CarrosModel>
    {
        public CarrosModelClassMap()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Cor).Index(1);
            Map(m => m.Marca).Index(2);
            Map(m => m.Modelo).Index(3);
            Map(m => m.Ano).Index(4);
        }
    }
}
