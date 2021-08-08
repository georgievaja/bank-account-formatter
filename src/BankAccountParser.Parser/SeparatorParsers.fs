namespace BankAccountParser.Parser

open WhiteSpaceParsers
open FParsec
open AbstractSyntaxTree

module SeparatorParsers =
    let pPrefixSeparator: Parser<Separator, unit> =
        pcharws '-'
        |>> fun _ -> PrefixSeparator

    let pBankCodeSeparator: Parser<Separator, unit> =
        pcharws '/'
        |>> fun _ -> BankCodeSeparator
        
    let pSeparator : Parser<BankAccountFormatPart, unit> =
        choicews [
            pPrefixSeparator
            pBankCodeSeparator
        ] |>> BankAccountSeparator