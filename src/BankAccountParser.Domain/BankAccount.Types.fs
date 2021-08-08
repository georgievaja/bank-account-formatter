namespace BankAccountParser.Types
open System

module BankAccount =

    module PublicTypes =
        
        type BankAccount = 
            {
                Prefix: int32
                AccountNumber: int64
                BankCode: int16
            }