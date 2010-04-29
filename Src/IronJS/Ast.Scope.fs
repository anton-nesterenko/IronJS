﻿namespace IronJS.Ast

open IronJS
open IronJS.Aliases

type LocalMap = Map<string, LocalVar>
type ClosureMap = Map<string, ClosureVar>

type ScopeFlags 
  = HasDS = 1
  | InLocalDS = 2
  | NeedGlobals = 4
  | NeedClosure = 8

type FuncScope = {
  LocalVars: LocalMap
  ClosureVars: ClosureMap
  ScopeLevel: int
  Flags: ScopeFlags Set
  ArgTypes: ClrType array
} with

  static member New = { 
    Flags = Set.empty
    LocalVars = Map.empty
    ClosureVars = Map.empty
    ScopeLevel = 0
    ArgTypes = null
  }

module Scope =

  let internal setFlag (f:ScopeFlags) (fs:FuncScope) =
    if Set.contains f fs.Flags then fs else {fs with Flags = Set.add f fs.Flags}

  let internal setFlagIf (f:ScopeFlags) (if':bool) (fs:FuncScope) =
    if Set.contains f fs.Flags then fs elif if' then {fs with Flags = Set.add f fs.Flags} else fs

  let internal delFlag (f:ScopeFlags) (fs:FuncScope) =
    if Set.contains f fs.Flags then {fs with Flags = Set.remove f fs.Flags} else fs

  let internal hasDynamicScope (fs:FuncScope) =
    fs.Flags.Contains ScopeFlags.HasDS

  let internal definedInLocalDynamicScope (fs:FuncScope) =
    fs.Flags.Contains ScopeFlags.InLocalDS

  let internal setClosure (fs:FuncScope) (name:string) (cv:ClosureVar) = 
    {fs with ClosureVars = Map.add name cv fs.ClosureVars}

  let internal hasClosure (scope:FuncScope) name = 
    Map.containsKey name scope.ClosureVars

  let internal setLocal (scope:FuncScope) (name:string) (lv:LocalVar) = 
    {scope with LocalVars = Map.add name lv scope.LocalVars}

  let internal hasLocal (scope:FuncScope) name = 
    Map.containsKey name scope.LocalVars