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
    public class ContactService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public ContactService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CreateContact(CreateContactDto contact)
        {
            try
            {
                _dbContext.Contacts.Add(_mapper.Map<Contact>(contact));
                _dbContext.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool DeleteContact(int id)
        {
            var res = _dbContext.Contacts.Find(id);
            if(res != null)
            {
                _dbContext.Contacts.Remove(res);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Contact> GetAllContacts()
        {
            return _dbContext.Contacts.ToList();
        }

        public Contact GetContactById(int id) 
        {
            return _dbContext.Contacts.Find(id);
        }
    }
}
