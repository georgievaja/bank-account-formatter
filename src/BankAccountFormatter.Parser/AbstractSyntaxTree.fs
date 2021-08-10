namespace BankAccountFormatter.Parser


module AbstractSyntaxTree =

    type Prefix =
        | PaddedPrefix
        | MinimizedPrefix
    
    type AccountNumber =
        | PaddedAccountNumber
        | MinimizedAccountNumber
        
    type BankCode =
        | PaddedBankCode
        | MinimizedBankCode
        
    type BankAccountPart =
        | Prefix of Prefix
        | AccountNumber of AccountNumber
        | BankCode of BankCode

    type Separator =
        | PrefixSeparator
        | BankCodeSeparator
        
    type BankAccountFormatPart =
        | BankAccountSeparator of Separator
        | BankAccountPart of BankAccountPart
        | OtherChar of char
        
    type BankAccountFormat =
        | BankAccountFormatParts of BankAccountFormatPart list