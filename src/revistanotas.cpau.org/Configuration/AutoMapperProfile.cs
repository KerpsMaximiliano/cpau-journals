using AutoMapper;

namespace CPAU.RevistaNotas.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Revista, Models.RevistasViewModels.RevistaViewModel>().MaxDepth(1)
                .ForMember(x => x.Contenidos, opt => opt.MapFrom(s => s.Items));
            CreateMap<Data.RevistaContenido, Models.RevistasViewModels.RevistaContenidoViewModel>().MaxDepth(1);
            CreateMap<Data.Contenido, Models.RevistasViewModels.ContenidoViewModel>().MaxDepth(1);
            CreateMap<Data.ContenidoAutor, Models.RevistasViewModels.ContenidoAutorViewModel>().MaxDepth(1);
            CreateMap<Data.Autor, Models.AutoresViewModel.AutorViewModel>().MaxDepth(1);
            CreateMap<Data.Contenido, Models.NotasViewModels.IndexViewModel>().MaxDepth(1);
        }
    }
}
