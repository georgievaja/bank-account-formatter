namespace BankAccountFormatter.Parser
open FParsec
open AbstractSyntaxTree

module CharParsers =

    let pOtherChar : Parser<BankAccountFormatPart, unit> =
        anyChar
        |>> OtherChar