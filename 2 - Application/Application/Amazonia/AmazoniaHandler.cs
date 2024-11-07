using API.Application.Dto;
using API.Application.Dto.Request;
using API.Application.Dto.ResponsePatterns;
using API.Domain.Interfaces.Write;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.AmazoniaHandler
{
    public class AmazoniaHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AmazoniaHandler(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
        {
            _uow = uow;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<ArvoreDto>> GetAll()
        {
            var consultaBase = _uow.ArvoreRepository.Find(x => !x.IsDeleted).AsQueryable();

            consultaBase = consultaBase.OrderByDescending(x => x.UpdateDate).ThenByDescending(x => x.InsertDate);

            var totalItens = await consultaBase.CountAsync();

            //mapeia o que quer retornar
            var itensPaginados = await consultaBase
                .Select(x => new ArvoreDto
                {
                    //Id = x.Id,
                    //Text = x.Text,
                    //Integer = x.Integer
                })
                .ToListAsync();

            return itensPaginados;
        }

        public async Task<ArvoreDto> GetById(Guid id)
        {
            var consultaBase = _uow.ArvoreRepository.Find(x => !x.IsDeleted && x.ArvoreID == id).FirstOrDefault();

            if (consultaBase != null)
            {
                var arvore = new ArvoreDto
                {

                };
                return arvore;
            }

            return null;
        }

        public async Task<bool> Update(ArvoreDto dto)
        {
            try
            {
                var arvore = _uow.ArvoreRepository.Find(p => p.ArvoreID == dto.ArvoreID).FirstOrDefault();

                var insert = false;

                if (arvore == null)
                {
                    insert = true;
                    arvore = new Arvore { ArvoreID = Guid.NewGuid() };
                }

                arvore.Nome = dto.Nome;
                arvore.PotenciaisBioprodutos = dto.PotenciaisBioprodutos;
                arvore.Transplante = dto.Transplante;
                arvore.DadosNutricionais = dto.DadosNutricionais;
                arvore.AproveitamentoAlimentacao = dto.AproveitamentoAlimentacao;
                arvore.AproveitamentoBiotecnologico = dto.AproveitamentoBiotecnologico;
                arvore.AspectosEcologicos = dto.AspectosEcologicos;
                arvore.Bioatividade = dto.Bioatividade;
                arvore.ColheitaBeneficiamentoSementes = dto.ColheitaBeneficiamentoSementes;
                arvore.Composicao = dto.Composicao;
                arvore.CuidadosAgua = dto.CuidadosAgua;
                arvore.CuidadosSolo = dto.CuidadosSolo;
                arvore.OcorrenciaNatural = dto.OcorrenciaNatural;
                arvore.Frutificacao = dto.Frutificacao;
                arvore.FormasDeConsumo = dto.FormasDeConsumo;
                arvore.Paisagismo = dto.Paisagismo;
                arvore.RegeneracaoNatural = dto.RegeneracaoNatural;
                arvore.DescricaoBotanica = dto.DescricaoBotanica;
                arvore.Dispersao = dto.Dispersao;
                arvore.ProducaoMudas = dto.ProducaoMudas;
                arvore.IsDeleted = false;

                if (insert)
                {
                    arvore.InsertDate = DateTime.Now;
                    _uow.ArvoreRepository.Insert(arvore);
                }
                else
                {
                    arvore.UpdateDate = DateTime.UtcNow;
                    _uow.ArvoreRepository.Update(arvore);
                }

                await _uow.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var generic = _uow.ArvoreRepository.Find(x => x.ArvoreID == id).FirstOrDefault();
            if (generic != null)
            {
                generic.UpdateDate = DateTime.UtcNow;
                generic.IsDeleted = true;

                _uow.ArvoreRepository.Update(generic);
                await _uow.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
