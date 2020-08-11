using System;
using System.ComponentModel.DataAnnotations;
using Motel.Models.API.Bases;
using Newtonsoft.Json;

namespace Motel.Models.API.ElectrictyAndWaters
{
    public class ElectrictyAndWaterRoomsNotInputRequest : RequestBase
    {
        [Required]
        [JsonProperty("MaNhaTro")]
        public int MaNhaTro { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty("NgayThangGhiSo")]
        public DateTime NgayThangGhiSo { get; set; }
    }

    public class InfoElectrictyAndWaterRequest : RequestBase
    {
        [JsonProperty("ElectrictyAndWaterDto")]
        public ElectrictyAndWaterDto ElectrictyAndWaterDto {get; set;}
    }

    public class InfoElectrictyAndWaterResponse : ResponseBase
    {}

    public class ElectrictyAndWaterOldRespone : ResponseBase
    {
        [JsonProperty("ElectrictyAndWaterIndexDto")]
        public ElectrictyAndWaterIndexDto ElectrictyAndWaterIndexDto { get; set; }
    }

    public class ElectrictyAndWaterOldRequest : RequestBase
    {
        [JsonProperty("MaPhong")]
        public int MaPhong { get; set; }
    }

    public class ElectrictyAndWaterIndexDto
    {
        [JsonProperty("ChiSoDien")]
        public int ChiSoDien { get; set; }

        [JsonProperty("ChiSoNuoc")]
        public int ChiSoNuoc { get; set; }
    }


}
