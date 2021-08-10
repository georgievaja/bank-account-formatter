namespace BankAccountFormatter.Parser

open FParsec
open AbstractSyntaxTree

module SeparatorParsers =
    let pPrefixSeparator: Parser<Separator, unit> =
        pchar '-'
        |>> fun _ -> PrefixSeparator

    let pBankCodeSeparator: Parser<Separator, unit> =
        pchar '/'
        |>> fun _ -> BankCodeSeparator
        
    let pSeparator : Parser<BankAccountFormatPart, unit> =
        choice [
            pPrefixSeparator
            pBankCodeSeparator
        ] |>> BankAccountSeparator