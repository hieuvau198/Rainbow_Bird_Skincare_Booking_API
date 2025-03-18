using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class BlogCommentMappingProfile : Profile
    {
        public BlogCommentMappingProfile()
        {
            // Entity to DTO (full)
            CreateMap<BlogComment, BlogCommentDto>()
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.InverseParentComment));

            // Entity to DTO (short version for replies)
            CreateMap<BlogComment, ShortBlogCommentDto>();

            // Create DTO to Entity
            CreateMap<CreateBlogCommentDto, BlogComment>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Upvotes, opt => opt.Ignore())
                .ForMember(dest => dest.Downvotes, opt => opt.Ignore())
                .ForMember(dest => dest.IsEdited, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.InverseParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.Blog, opt => opt.Ignore())
                .ForMember(dest => dest.ParentComment, opt => opt.Ignore());

            // Update DTO to Entity
            CreateMap<UpdateBlogCommentDto, BlogComment>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Upvotes, opt => opt.Ignore())
                .ForMember(dest => dest.Downvotes, opt => opt.Ignore())
                .ForMember(dest => dest.IsEdited, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.InverseParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.Blog, opt => opt.Ignore())
                .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.BlogId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCommentId, opt => opt.Ignore())
                .ForMember(dest => dest.FullName, opt => opt.Ignore())
                .ForMember(dest => dest.CommentId, opt => opt.Ignore());
        }
    }
}
