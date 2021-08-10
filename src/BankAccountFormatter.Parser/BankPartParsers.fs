namespace BankAccountFormatter.Parser

open FParsec
open AbstractSyntaxTree
open CharParsers

module BankPartParsers =
    
    let pPaddedPrefix: Parser<Prefix, unit> =
        pchar 'P'
        |>> fun _ -> PaddedPrefix

    let pMinimizedPrefix: Parser<Prefix, unit> =
        pchar 'p'
        |>> fun _ -> MinimizedPrefix

    let pPrefix : Parser<BankAccountPart, unit> =
        choice [
            pMinimizedPrefix
            pPaddedPrefix
        ] |>> Prefix
        
    let pPaddedAccountNumber: Parser<AccountNumber, unit> =
        pchar 'A'
        |>> fun _ -> PaddedAccountNumber

    let pMinimizedAccountNumber: Parser<AccountNumber, unit> =
        pchar 'a'
        |>> fun _ -> MinimizedAccountNumber

    let pAccountNumber : Parser<BankAccountPart, unit> =
        choice [
            pMinimizedAccountNumber
            pPaddedAccountNumber
        ] |>> AccountNumber
        
    let pPaddedBankCode: Parser<BankCode, unit> =
        pchar 'B'
        |>> fun _ -> PaddedBankCode

    let pMinimizedBankCode: Parser<BankCode, unit> =
        pchar 'b'
        |>> fun _ -> MinimizedBankCode

    let pBankCode : Parser<BankAccountPart, unit> =
        choice [
            pMinimizedBankCode
            pPaddedBankCode
        ] |>> BankCode

    let pBankAccountPart : Parser<BankAccountFormatPart, unit> =
        choice [
            pPrefix
            pAccountNumber
            pBankCode
        ] |>> BankAccountPart
