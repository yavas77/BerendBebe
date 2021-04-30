using AutoMapper;
using BerendBebe.DTO.CartDtos;
using BerendBebe.DTO.CategoryDtos;
using BerendBebe.DTO.CommentDtos;
using BerendBebe.DTO.ContactDtos;
using BerendBebe.DTO.OrderDtos;
using BerendBebe.DTO.ProductDtos;
using BerendBebe.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.WebUI.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region CategoryMaps
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();

            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();

            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
            #endregion

            #region ProductMaps
            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, ProductAddDto>();

            CreateMap<ProductListDto, Product>();
            CreateMap<Product, ProductListDto>()
                .ForMember(dto => dto.CategoryName,
                map => map.MapFrom(entity => entity.Category.CategoryName));

            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();

            CreateMap<ProductImageAddDto, Product>();
            CreateMap<Product, ProductImageAddDto>()
                .ForMember(dto => dto.ProductId,
                map => map.MapFrom(entity => entity.Id));

            CreateMap<ProductImageAddDto, ProductImage>();
            CreateMap<ProductImage, ProductImageAddDto>();

            CreateMap<ProductImageDetailListDto, ProductImage>();
            CreateMap<ProductImage, ProductImageDetailListDto>();

            CreateMap<ProductDetailListDto, Product>();
            CreateMap<Product, ProductDetailListDto>();

            CreateMap<ProductImageListDto, Product>();
            CreateMap<Product, ProductImageListDto>();
            #endregion

            #region CartMaps
            CreateMap<CartAddDto, Cart>();
            CreateMap<Cart, CartAddDto>();

            CreateMap<CartListDto, Cart>();
            CreateMap<Cart, CartListDto>()
                .ForMember(dto => dto.ShowImageUrl,
                map => map.MapFrom(entity => entity.Product.ShowImageUrl))
                .ForMember(dto => dto.ProductName,
                map => map.MapFrom(entity => entity.Product.ProductName))
                .ForMember(dto => dto.Price,
                map => map.MapFrom(entity => entity.Product.Price));

            CreateMap<CartDeleteDto, Cart>();
            CreateMap<Cart, CartDeleteDto>();
            #endregion

            #region >OrderMaps
            CreateMap<OrderAddDto, Order>();
            CreateMap<Order, OrderAddDto>();

            CreateMap<ConfirmOrderDto, Order>();
            CreateMap<Order, ConfirmOrderDto>();

            CreateMap<OrderAddDto, OrderDetail>();
            CreateMap<OrderDetail, OrderAddDto>();

            CreateMap<OrderAdminListDto, Order>();
            CreateMap<Order, OrderAdminListDto>();

            CreateMap<OrderUpdateAdminDto, Order>();
            CreateMap<Order, OrderUpdateAdminDto>();

            CreateMap<OrderAdminDetailDto, Order>();
            CreateMap<Order, OrderAdminDetailDto>()
                .ForMember(dto => dto.PaymentTypeName,
                map => map.MapFrom(entity => entity.PaymentType.PaymentName));
            #endregion


            #region CommentMaps
            CreateMap<CommentListDto, Comment>();
            CreateMap<Comment, CommentListDto>();

            CreateMap<CommentAddDto, Comment>();
            CreateMap<Comment, CommentAddDto>();

            CreateMap<CommentAdminListDto, Comment>();
            CreateMap<Comment, CommentAdminListDto>()
                .ForMember(dto => dto.ProductName,
                map => map.MapFrom(entity => entity.Product.ProductName));

            CreateMap<CommentDetailAdminDto, Comment>();
            CreateMap<Comment, CommentDetailAdminDto>()
                .ForMember(dto => dto.ProductName,
                map => map.MapFrom(entity => entity.Product.ProductName));

            #endregion

            #region ContactMaps

            CreateMap<ContactListAdminDto, Contact>();
            CreateMap<Contact, ContactListAdminDto>();

            CreateMap<ContactSendDto, Contact>();
            CreateMap<Contact, ContactSendDto>();

            CreateMap<ContactDetailAdminDto, Contact>();
            CreateMap<Contact, ContactDetailAdminDto>();

            CreateMap<ContactReplyDto, Contact>();
            CreateMap<Contact, ContactReplyDto>();

            CreateMap<ContactComposeDto, Contact>();
            CreateMap<Contact, ContactComposeDto>();

            CreateMap<SendedMailListDto, Contact>();
            CreateMap<Contact, SendedMailListDto>(); 

            #endregion

        }
    }
}
