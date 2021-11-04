using ModelApp.Dto.AnuncioInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Interface
{
    public interface IAnuncioInfoRepository
    {
        Task<IEnumerable<AnuncioInfoConsultarDto>> GetAllAnuncioInfo();
        Task<AnuncioInfoConsultarDto> GetByIdAnuncioInfo(AnuncioInfoModificarDto model);
        Task<int> InsertAnuncioInfo(AnuncioInfoCrearDto AnuncioInfoCrearDto);
        Task<int> UpdateAnuncioInfo(AnuncioInfoModificarDto AnuncioInfoCrearDto);
        Task<int> DeleteAnuncioInfo(int ID);

        Task<IEnumerable<AnuncioInfoConsultarDto>> GetAllAnuncioBySearch( SearchBussinesRequest search);
    }
}
