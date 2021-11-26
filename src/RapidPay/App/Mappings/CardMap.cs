using AutoMapper;
using RapidPay.Models.Dto;
using RapidPay.Models.Entities;

namespace RapidPay.App.Mappings
{
    public class CardMap : Profile
    {
        public CardMap()
        {
            CreateMap<CardDto, Card>();

            CreateMap<Card, CardDto>();

        }
    }
}
