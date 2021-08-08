namespace BankAccountParser.Interpreter
open BankAccountParser.Parser.AbstractSyntaxTree
open BankAccountParser.Teams.BankAccount.PublicTypes
open BankAccountParser.Teams.BankAccount

module Types =
    type FormattingFunction = DomesticBankAccount -> string

    type UnexpectedFailureType =
        | UnsupportedExistsExpression
        | UnsupportedPredicateOperator
        | UnsupportedQueryExpression

    type InterpreterFailure =
        | Unexpected of UnexpectedFailureType

module Interpreter =
    open Monads
    open Types
    
    let interpretPrefix (prefix: Prefix) : FormattingFunction =
        match prefix with
        | MinimizedPrefix -> (fun ds -> ds.Prefix.ToString())
        | PaddedPrefix -> (fun ds -> ds.Prefix.ToString().PadLeft(6, '0'))
    
    let interpretBankCode (bankCode: BankCode) : FormattingFunction =
        match bankCode with
        | MinimizedBankCode -> (fun ds -> ds.BankCode.ToString())
        | PaddedBankCode -> (fun ds -> ds.BankCode.ToString().PadLeft(4, '0'))
        
    let interpretAccountNumber (accountNumber: AccountNumber) : FormattingFunction =
        match accountNumber with
        | MinimizedAccountNumber -> (fun ds -> ds.AccountNumber.ToString())
        | PaddedAccountNumber -> (fun ds -> ds.AccountNumber.ToString().PadLeft(10, '0'))
        
    let interpretBankAccountPart (bankAccountPart: BankAccountPart) : FormattingFunction =
        match bankAccountPart with
        | Prefix p -> interpretPrefix p
        | AccountNumber an -> interpretAccountNumber an
        | BankCode bc -> interpretBankCode bc
    
    let interpretSeparator (separator: Separator) : FormattingFunction =
        match separator with
        | BankCodeSeparator -> (fun _ -> "/")
        | PrefixSeparator -> (fun _ -> "-")
        
    let interpretBankAccountFormatPart (part : BankAccountFormatPart) : FormattingFunction =
        match part with
        | BankAccountPart ba -> interpretBankAccountPart ba
        | BankAccountSeparator se -> interpretSeparator se
        
    let interpret (format: BankAccountFormat) : FormattingFunction =
        fun domesticBankAccount ->
        match format with
            | BankAccountFormatParts bankAccountFormatParts ->
                   bankAccountFormatParts
                    |> List.map interpretBankAccountFormatPart        
                    |> List.map (fun x -> x domesticBankAccount)
                    |> List.fold (+) ""
                
