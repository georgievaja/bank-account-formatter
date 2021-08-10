namespace BankAccountFormatter.Parser

open FParsec

module WhiteSpaceParsers = 
    let pcharws ch  = spaces >>. pchar ch .>> spaces
    let choicews ps = spaces >>. choice ps .>> spaces
