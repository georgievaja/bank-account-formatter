namespace BankAccountParser.Parser

open FParsec
open AbstractSyntaxTree
open ParserTypes.Monads
open ParserTypes.Types
open WhiteSpaceParsers
open BankPartParsers
open SeparatorParsers

module Parser =


                
    let pBankAccountFormatQuery : Parser<BankAccountFormatPart, unit> = 
        choicews [
            pBankAccountPart
            pSeparator
        ]

    let pBankAccountFormatFailFatally: Parser<BankAccountFormatPart, _> =
        fun stream ->
            let reply = pBankAccountFormatQuery stream
            match reply.Status with
                | Ok -> Reply(reply.Result)             
                | _ -> Reply(FatalError, reply.Error)
  
    let pBankAccountFormat : Parser<BankAccountFormat, unit> = 
        many1Till pBankAccountFormatFailFatally eof
        |>> BankAccountFormatParts

    let parse (rule: string) : ParsingResult<BankAccountFormat, OperationLog> = 
      match run pBankAccountFormat rule with
       | ParserResult.Success (result, _, _) ->
           Success result
       | ParserResult.Failure (_, error, _) -> 
            SyntacticLog.create rule error
            |> Syntactic
            |> Failure

                                                    

