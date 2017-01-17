// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtendedXmlSerialization.Conversion;
using ExtendedXmlSerialization.Core.Sources;
using ExtendedXmlSerialization.Core.Specifications;
using ExtendedXmlSerialization.ElementModel.Members;

namespace ExtendedXmlSerialization.ElementModel
{
    class Elements : Selector<TypeInfo, IElement>, IElements
    {
        public static Elements Default { get; } = new Elements();
        Elements() : this(ElementNames.Default, ElementMembers.Default, Defaults.Names.Select(x => x.Classification)) {}

        protected Elements(IElementNames names, IElementMembers members, IEnumerable<TypeInfo> registered) : this(
            new ElementOption(
                new AnySpecification<TypeInfo>(new DelegatedSpecification<TypeInfo>(registered.Contains),
                                               IsAssignableSpecification<Enum>.Default), names),
            new DictionaryElementOption(names, members),
            new ArrayElementOption(names),
            new CollectionElementOption(names, members),
            new ActivatedElementOption(names, members), 
            new ElementOption(names)) {}

        protected Elements(params IOption<TypeInfo, IElement>[] options) : base(options) {}
    }
}