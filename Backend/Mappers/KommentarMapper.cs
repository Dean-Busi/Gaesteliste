using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class KommentarMapper
    {
        public static KommentarDto ToKommentarDto(this Kommentar kommentarModel)
        {
            return new KommentarDto
            {
                GepostedVon = kommentarModel.GepostedVon,
                Inhalt = kommentarModel.Inhalt,
                GepostetAm = kommentarModel.GepostetAm
            };
        }

        public static Kommentar ToKommentarFromCreateDto(this CreateKommentarDto createKommentarDto)
        {
            return new Kommentar
            {
                GepostedVon = createKommentarDto.GepostedVon,
                Inhalt = createKommentarDto.Inhalt,
            };
        }

        public static Kommentar ToKommentarFromUpdateDto(this UpdateKommentarDto updateKommentarDto)
        {
            return new Kommentar
            {
                Inhalt = updateKommentarDto.Inhalt,
            };
        }
    }
}

