namespace BankAccountParser.Parser

open WhiteSpaceParsers
open FParsec
open AbstractSyntaxTree
open CharParsers

module BankPartParsers =
    
    let pPaddedPrefix: Parser<Prefix, unit> =
        pcharws 'P'
        |>> fun _ -> PaddedPrefix

    let pMinimizedPrefix: Parser<Prefix, unit> =
        pcharws 'p'
        |>> fun _ -> MinimizedPrefix

    let pPrefix : Parser<BankAccountPart, unit> =
        choicews [
            pMinimizedPrefix
            pPaddedPrefix
        ] |>> Prefix
        
    let pPaddedAccountNumber: Parser<AccountNumber, unit> =
        pcharws 'A'
        |>> fun _ -> PaddedAccountNumber

    let pMinimizedAccountNumber: Parser<AccountNumber, unit> =
        pcharws 'a'
        |>> fun _ -> MinimizedAccountNumber

    let pAccountNumber : Parser<BankAccountPart, unit> =
        choicews [
            pMinimizedAccountNumber
            pPaddedAccountNumber
        ] |>> AccountNumber
        
    let pPaddedBankCode: Parser<BankCode, unit> =
        pcharws 'B'
        |>> fun _ -> PaddedBankCode

    let pMinimizedBankCode: Parser<BankCode, unit> =
        pcharws 'b'
        |>> fun _ -> MinimizedBankCode

    let pBankCode : Parser<BankAccountPart, unit> =
        choicews [
            pMinimizedBankCode
            pPaddedBankCode
        ] |>> BankCode

    let pBankAccountPart : Parser<BankAccountFormatPart, unit> =
        choicews [
            pPrefix
            pAccountNumber
            pBankCode
        ] |>> BankAccountPart
