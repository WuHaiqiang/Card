﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.Dto
{
    [Serializable]
    public class AccountDto
    {
        public string Account;
        public string Password;

        public AccountDto()
        {

        }

        public AccountDto(string account, string password)
        {
            this.Account = account;
            this.Password = password;
        }
    }
}
