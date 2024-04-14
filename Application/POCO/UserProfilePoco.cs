using Application.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.POCO
{
    public class UserProfilePoco
    {
       public UserProfilePoco() { }
      public  UserProfileDto UserProfilePocoModel(UserProfileEntity u) {
            UserProfileDto ud = new()
            {
                UserId = u.UserId,
                DateofBirth = u.DateofBirth,
                Gender = u.Gender,
                MaritalStatus = u.MaritalStatus,
                ResidentialAddress = u.ResidentialAddress,
                Town = u.Town,
                BusinessLocation = u.BusinessLocation,
                BVN = u.BVN,
                City = u.City,
                Country = u.Country,
                Maidenname = u.Maidenname,
                NIN = u.NIN,
                Phone = u.Phone,
                PostalCode = u.PostalCode,
                Region = u.Region,
                ResidentPerminNo = u.ResidentPerminNo,
                SignatureUrl = u.SignatureUrl,
                Stateoforigin = u.Stateoforigin,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            };
            return ud;

        }
       public  UserProfileEntity UserProfilePocoDto(UserProfileDto u)
        {
            UserProfileEntity ud = new()
            {
                UserId = u.UserId,
                DateofBirth = u.DateofBirth,
                Gender = u.Gender,
                MaritalStatus = u.MaritalStatus,
                ResidentialAddress = u.ResidentialAddress,
                Town = u.Town,
                BusinessLocation = u.BusinessLocation,
                BVN = u.BVN,
                City = u.City,
                Country = u.Country,
                Maidenname = u.Maidenname,
                NIN = u.NIN,
                Phone = u.Phone,
                PostalCode = u.PostalCode,
                Region = u.Region,
                ResidentPerminNo = u.ResidentPerminNo,
                SignatureUrl = u.SignatureUrl,
                Stateoforigin = u.Stateoforigin,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email

            };
            return ud;
        }

        public UserProfileEntity UserProfilePocoDtoNull()
        {
            UserProfileEntity ud = new()
            {
                UserId = "",
                DateofBirth = "",
                Gender = "",
                MaritalStatus = "",
                ResidentialAddress = "",
                Town = "",
                BusinessLocation = "",
                BVN = "",
                City = "",
                Country = "",
                Maidenname = "",
                NIN = "",
                Phone = "",
                PostalCode = "",
                Region = "",
                ResidentPerminNo = "",
                SignatureUrl = "",
                Stateoforigin = "",
            };
            return ud;
        }

        public UserProfileDto UserProfilePocoModelNull()
        {
            UserProfileDto ud = new()
            {
                UserId = "",
                DateofBirth = "",
                Gender = "",
                MaritalStatus = "",
                ResidentialAddress = "",
                Town = "",
                BusinessLocation = "",
                BVN = "",
                City = "",
                Country = "",
                Maidenname = "",
                NIN = "",
                Phone = "",
                PostalCode = "",
                Region = "",
                ResidentPerminNo = "",
                SignatureUrl = "",
                Stateoforigin ="",
            };
            return ud;
        }
    }
}
