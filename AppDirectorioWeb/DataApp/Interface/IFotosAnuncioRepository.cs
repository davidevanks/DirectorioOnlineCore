using ModelApp.Dto.FotosAnuncio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApp.Interface
{
    public interface IFotosAnuncioRepository
    {
        Task<IEnumerable<FotosAnuncioConsultarDto>> GetAllFotosAnuncio(int ID);
        Task<FotosAnuncioConsultarDto> GetByIdFotosAnuncio(int ID);
        Task<int> InsertFotosAnuncio(FotosAnuncioCrearDto FotosAnuncioCrearDto);
        Task<int> UpdateFotosAnuncio(FotosAnuncioModificarDto FotosAnuncioCrearDto);
        Task<int> DeleteFotosAnuncio(int ID);
    }
}
