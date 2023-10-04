using AutoMapper;
using MovieStoreWebapi.Application.ActorOperations.Commands.CreateActor;
using MovieStoreWebapi.Application.ActorOperations.Commands.UpdateActor;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStoreWebapi.Application.ActorOperations.Queries.GetActors;
using MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebapi.Application.CustomerOperations.Commands.UpdateCustomer;
using MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.CreateDirectorMovie;
using MovieStoreWebapi.Application.DirectorMovieOperations.Commands.UpdateDirectorMovie;
using MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovieDetail;
using MovieStoreWebapi.Application.DirectorMovieOperations.Queries.GetDirectorMovies;
using MovieStoreWebapi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStoreWebapi.Application.DirectorOperations.Queries.Get;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.CreateFavoriteGenre;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Commands.UpdateFavoriteGenre;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenreDetail;
using MovieStoreWebapi.Application.FavoriteGenreOprations.Queries.GetFavoriteGenres;
using MovieStoreWebapi.Application.GenreOperations.Commands.CreateGenre;
using MovieStoreWebapi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStoreWebapi.Application.GenreOperations.Queries;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.CreateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Commands.UpdateMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActor;
using MovieStoreWebapi.Application.MovieActorOperations.Queries.GetMovieActorDetail;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.CreateMovieGenre;
using MovieStoreWebapi.Application.MovieGenreOperations.Commands.UpdateMovieGenre;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenreDetail;
using MovieStoreWebapi.Application.MovieGenreOperations.Queries.GetMovieGenres;
using MovieStoreWebapi.Application.MovieOperations.Commands.CreateMovie;
using MovieStoreWebapi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStoreWebapi.Application.MovieOperations.Queries.GetMovies;
using MovieStoreWebapi.Application.OrderOperations.Commands.CreateOrder;
using MovieStoreWebapi.Application.OrderOperations.Commands.UpdateOrder;
using MovieStoreWebapi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebapi.Application.OrderOperations.Queries.GetOrders;
using MovieStoreWebapi.Entities;
using static MovieStoreWebapi.Application.ActorOperations.Queries.GetActorDetail.GetActorDetailQuery;
using static MovieStoreWebapi.Application.CustomerOperations.Queries.GetCustomerDetail.GetCustomerDetailQuery;
using static MovieStoreWebapi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;
using static MovieStoreWebapi.Application.DirectorOperations.Queries.GetDirectorDetail.GetDirectorDetailQuery;

namespace MovieStoreWebapi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {    
           
        
           /*                      Movie                                */
            CreateMap<UpdateMovieViewModel, Movie>().ReverseMap();
            CreateMap<CreateMovieViewModel, Movie>().ReverseMap();
            // Movie sınıfından GetMovieQueryViewModel'e haritalama
            CreateMap<Movie, GetMovieDetailQueryViewModel>()
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName+ " " + src.Director.LastName));
             // MovieDetailViewModel'den Movie sınıfına ters haritalama
              CreateMap<GetMovieDetailQueryViewModel, Movie>() 
             .ForMember(dest => dest.Director, opt => opt.Ignore());
            
             CreateMap<Movie, GetMoviesQueryViewModel>()
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FirstName+" "+src.Director.LastName));
            CreateMap<GetMoviesQueryViewModel, Movie>()
            .ForMember(dest => dest.Director, opt => opt.Ignore());

            /*                      Customer                                */
            CreateMap<CreateCustomerViewModel,Customer>().ReverseMap();
            CreateMap<UpdateCustomerViewModel,Customer>().ReverseMap();
            CreateMap<Customer, GetCustomersViewModel>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(m => m.Orders.Select(s =>"Sipariş Id :"+s.Id+",  Sipariş Edilen Film :"+s.Movie.Title+",  Sipariş edilen tarih :"+s.PurchaseDate)))
            .ForMember(dest => dest.FavoriteGenres, opt => opt.MapFrom(m => m.FavoriteGenres.Select(s =>"Id :"+s.Id+", Favori Film Türü :"+s.Genre.Name)));
             CreateMap<Customer, GetCustomerDetailViewModel>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(m => m.Orders.Select(s =>"Sipariş Id :"+s.Id+",  Sipariş Edilen Film :"+s.Movie.Title+",  Sipariş edilen tarih :"+s.PurchaseDate)))
            .ForMember(dest => dest.FavoriteGenres, opt => opt.MapFrom(m => m.FavoriteGenres.Select(s =>"Id :"+s.Id+", Favori Film Türü :"+s.Genre.Name)));

            /*                      Genre                                */
            CreateMap<CreateGenreViewModel, Genre>().ReverseMap();
            CreateMap<UpdateGenreViewModel, Genre>().ReverseMap();
            CreateMap<Genre, GetGenreDetailViewModel>().ReverseMap();
            CreateMap<Genre,GetGenresViewModel>().ReverseMap();

            /*                      Actor                                */
            CreateMap<CreateActorViewModel, Actor>().ReverseMap();
            CreateMap<UpdateActorViewModel, Actor>().ReverseMap();
            CreateMap<Actor, GetActorDetailViewModel>()
            .ForMember(dest => dest.ActorMovies, opt => opt.MapFrom(m => m.ActorMovies.Select(s =>s.Id+":"+s.Movie.Title)));

             CreateMap<GetActorDetailViewModel, Actor>()
            .ForMember(dest => dest.ActorMovies, opt => opt.Ignore());  

            CreateMap<Actor,GetActorsViewModel>()
            .ForMember(dest => dest.ActorMovies, opt => opt.MapFrom(m => m.ActorMovies.Select(s =>s.Id+":"+s.Movie.Title)));

            CreateMap<GetActorsViewModel, Actor>()
            .ForMember(dest => dest.ActorMovies, opt => opt.Ignore());  

            /*                      Director                                */

            CreateMap<CreateDirectorViewModel, Director>().ReverseMap();
            CreateMap<UpdateDirectorViewModel, Director>().ReverseMap();
            CreateMap<Director,GetDirectorDetailViewModel>().ReverseMap();
            CreateMap<Director,GetDirectorsViewModel>().ReverseMap();

             /*                      Order                                */
            CreateMap<Order, GetOrderDetailQueryViewModel>()
            .ForMember(dest => dest.Customer,opt => opt.MapFrom(src => src.Customer.FirstName+" "+src.Customer.LastName))
            .ForMember(dest => dest.Movie,opt => opt.MapFrom(src => src.Movie.Title));

             CreateMap<GetOrderDetailQueryViewModel, Order>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore());

             CreateMap< Order,GetOrdersViewModel>()
             .ForMember(dest => dest.Customer,opt => opt.MapFrom(src => src.Customer.FirstName+" "+src.Customer.LastName))
             .ForMember(dest => dest.Movie,opt => opt.MapFrom(src => src.Movie.Title));

             CreateMap<GetOrdersViewModel, Order>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore());

            CreateMap<CreateOrderViewModel, Order>().ReverseMap();
            CreateMap<UpdateOrderViewModel, Order>().ReverseMap();

            /*                      MovieActor                                */

            CreateMap<CreateMovieActorViewModel, MovieActor>();   
            CreateMap<UpdateMovieActorViewModel, MovieActor>();  
        

             CreateMap<GetMovieActorDetailViewModel, Actor>()
            .ForMember(dest => dest.ActorMovies, opt => opt.Ignore());  

            CreateMap<Actor, GetMovieActorsViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(m => m.FirstName + " " + m.LastName))
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(m => m.ActorMovies.Select(s => s.Movie.Title)))
                .ForMember(dest => dest.MovieActorIds, opt => opt.MapFrom(m => m.ActorMovies.Select(m=>m.Id)));
                CreateMap<GetMovieActorsViewModel, Actor>()
                .ForMember(dest => dest.ActorMovies, opt => opt.Ignore()); 
                
                CreateMap<MovieActor, GetMovieActorsViewModel2>()
                .ForMember(dest => dest.Actor, opt => opt.MapFrom(m => m.Actor.FirstName + " " + m.Actor.LastName))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(s => s.Movie.Title));
               
                CreateMap<GetMovieActorsViewModel2, MovieActor>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.Actor, opt => opt.Ignore()); 

        
            CreateMap<Actor, GetMovieActorDetailViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(m => m.FirstName + " " + m.LastName))
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(m => m.ActorMovies.Select(s => s.Movie.Title)))
                .ForMember(dest => dest.MovieActorIds, opt => opt.MapFrom(m => m.ActorMovies.Select(m=>m.Id)));

                /*                      MovieGenre                              */

                CreateMap<CreateMovieGenreViewModel, MovieGenre>().ReverseMap();

                CreateMap<UpdateMovieGenreViewModel, MovieGenre>().ReverseMap();

                CreateMap<MovieGenre, GetMovieGenresViewModel2>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(s => s.Movie.Title))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(s => s.Genre.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(m=>m.Id));

                 CreateMap<GetMovieGenresViewModel2, MovieGenre>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore()); 

               /*                      FavoriteGenre                              */

                 CreateMap<CreateFavoriteGenreViewModel, FavoriteGenre>().ReverseMap();
                 CreateMap<UpdateFavoriteGenreViewModel, FavoriteGenre>().ReverseMap();
                 
                CreateMap<FavoriteGenre, GetFavoriteGenreDetailViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(s => s.Customer.FirstName+""+s.Customer.LastName))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(s => s.Genre.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(m=>m.Id));

                 CreateMap<GetFavoriteGenreDetailViewModel, FavoriteGenre>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore()); 

                CreateMap<FavoriteGenre, GetFavoriteGenresViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(s => s.Customer.FirstName+""+s.Customer.LastName))
                .ForMember(dest => dest.Genre ,opt => opt.MapFrom(s => s.Genre.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(m=>m.Id));

                CreateMap<GetFavoriteGenresViewModel, FavoriteGenre>()
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore()); 

                 /*                      DirectorMovie                                */

            CreateMap<CreateDirectorMovieViewModel,DirectorMovie >().ReverseMap();   
            CreateMap<UpdateDirectorMovieViewModel, DirectorMovie>().ReverseMap(); 

            CreateMap<DirectorMovie, GetDirectorMoviesViewModel>()
            .ForMember(dest => dest.Director, opt => opt.MapFrom(m => m.Director.FirstName + " " + m.Director.LastName))
            .ForMember(dest => dest.Movie, opt => opt.MapFrom(m => m.Movie.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(
            m=>m.Id));
        
        

             CreateMap<GetDirectorMoviesViewModel, DirectorMovie>()
            .ForMember(dest => dest.Movie, opt => opt.Ignore())
            .ForMember(dest => dest.Director, opt => opt.Ignore());  

            CreateMap<DirectorMovie, GetDirectorMovieDetailViewModel>()
                .ForMember(dest => dest.Director, opt => opt.MapFrom(m => m.Director.FirstName + " " + m.Director.LastName))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(m => m.Movie.Title))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(
                m=>m.Id));
                CreateMap<GetDirectorMovieDetailViewModel, DirectorMovie>()
                .ForMember(dest => dest.Movie, opt => opt.Ignore())
                .ForMember(dest => dest.Director, opt => opt.Ignore()); 


            
            //     CreateMap<Customer, GetMovieGenresViewModel>()
            //     .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.FirstName+" "+s.LastName))
            //    .ForMember(dest => dest.Genres, opt => opt.MapFrom(s => s.FavoritesGenres.Select(m=>m.Id+":"+m.Genre.Name)))
            //     .ForMember(dest => dest.MovieGenreIds, opt => opt.MapFrom(s => s.FavoritesGenres.Select(m=>m.Id)))
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom(m=>m.Id));

            //      CreateMap<GetMovieGenresViewModel, Customer>()
            //     .ForMember(dest => dest.FavoritesGenres, opt => opt.Ignore());

                
                // CreateMap<Customer, GetMovieGenreDetailViewModel>()
                // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.FirstName+" "+s.LastName))
                // .ForMember(dest => dest.Genres, opt => opt.MapFrom(s => s.FavoritesGenres.Select(m=>m.Id+":"+m.Genre.Name)))
                // .ForMember(dest => dest.MovieGenreIds, opt => opt.MapFrom(s => s.FavoritesGenres.Select(m=>m.Id)))
                // .ForMember(dest => dest.Id, opt => opt.MapFrom(m=>m.Id));
         








        }
    }

}