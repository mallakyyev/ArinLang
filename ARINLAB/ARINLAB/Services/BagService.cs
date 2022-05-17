using AutoMapper;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public class BagService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public BagService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CreateBag(CreateBagDto bag)
        {
            try
            {
                Bag b = _mapper.Map<Bag>(bag);
                b.Date = DateTime.Now;
                b.IsRead = false;
                _dbContext.Bags.Add(_mapper.Map<Bag>(bag));
                _dbContext.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public List<Bag> GetTopBags()
        {
            return _dbContext.Bags.Where(p => p.IsRead == false).OrderByDescending(p => p.Date).Take(5).ToList();
        }

        public bool DeleteBag(int id)
        {
            var res = _dbContext.Bags.Find(id);
            if(res != null)
            {
                _dbContext.Bags.Remove(res);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Bag> GetAllBags()
        {
            return _dbContext.Bags.OrderByDescending(p => p.Date).ToList();
        }

        public Bag GetBagById(int id)
        {
            return _dbContext.Bags.Find(id);
        }

        public bool Readed(int id)
        {
            try
            {
                var res = _dbContext.Bags.Find(id);
                if (res != null)
                {
                    res.IsRead = true;
                    _dbContext.Bags.Update(res);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
