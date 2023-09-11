## [7.1.1](https://github.com/dre0dru/AssetManagement/compare/v7.1.0...v7.1.1) (2023-09-11)


### Bug Fixes

* wrapped missing VContainer usage with defines ([2c73201](https://github.com/dre0dru/AssetManagement/commit/2c73201619d7cf78115d107d7733789aa3e744e8))

# [7.1.0](https://github.com/dre0dru/AssetManagement/compare/v7.0.0...v7.1.0) (2023-06-06)


### Features

* added scoped/transient assets ([c1ec7e6](https://github.com/dre0dru/AssetManagement/commit/c1ec7e6363a3c9b6c1b3b61520941c6f95a216c4))

# [7.0.0](https://github.com/dre0dru/AssetManagement/compare/v6.1.1...v7.0.0) (2022-11-29)


### Features

* package at root ([f0b2d73](https://github.com/dre0dru/AssetManagement/commit/f0b2d73e94f9d7238d6f8de5d519eb84b7c99dce))


### BREAKING CHANGES

* namespaces change

## [6.1.1](https://github.com/dre0dru/AddressablesServices/compare/v6.1.0...v6.1.1) (2022-06-01)


### Bug Fixes

* fixed invalid dependency on shared.sources ([4764de4](https://github.com/dre0dru/AddressablesServices/commit/4764de4ca0f29b80791ff1df68ecec19d253e24e))

# [6.1.0](https://github.com/dre0dru/AddressablesServices/compare/v6.0.0...v6.1.0) (2021-12-27)


### Features

* updated TMP fonts build processor ([a0ab4e1](https://github.com/dre0dru/AddressablesServices/commit/a0ab4e11e93b76a6bf8bf2c2bc696f511c0952de))

# [6.0.0](https://github.com/dre0dru/AddressablesServices/compare/v5.0.0...v6.0.0) (2021-12-19)


### Features

* reworked fonts service, generic assets ([cee22b2](https://github.com/dre0dru/AddressablesServices/commit/cee22b228267357b529a9e76e527703bdbf50457))


### BREAKING CHANGES

* fonts service signature changes

# [5.0.0](https://github.com/dre0dru/AddressablesServices/compare/v4.0.0...v5.0.0) (2021-12-19)


### Bug Fixes

* removed webgl preprocessor directive for asasets download pack ([c5ddab7](https://github.com/dre0dru/AddressablesServices/commit/c5ddab7231a00ead3fe36d9483b3ad9ccf1af8d1))


### Features

* updated code to support shared sources changes ([b2c069f](https://github.com/dre0dru/AddressablesServices/commit/b2c069fca8da692dc456a7ace339ede726ed150a))


### BREAKING CHANGES

* namespaces and interfaces were changed fonts, sprite atlases and databases

# [4.0.0](https://github.com/dre0dru/AddressablesServices/compare/v3.0.1...v4.0.0) (2021-12-06)


### Bug Fixes

* added missing lifetime parameter ([4c5f275](https://github.com/dre0dru/AddressablesServices/commit/4c5f27512e3b3ddc054220cdb6597fbfdcb0a0d4))
* added preprocessor for required member attribute ([e08fdb3](https://github.com/dre0dru/AddressablesServices/commit/e08fdb368dd994c79add5d50cafdd83301ac49dd))
* fonts editor assembly target platform wasn't set as editor ([ee81709](https://github.com/dre0dru/AddressablesServices/commit/ee817091f67fe9ee6139448e9193f6d46679f0c7))
* wrong preprocessor directive for sprite atlases ([cfe1060](https://github.com/dre0dru/AddressablesServices/commit/cfe10601927d7e85d02a136b828e4f0d5af8b440))


### Code Refactoring

* renaming ([adad578](https://github.com/dre0dru/AddressablesServices/commit/adad578e832177aeda6b3128217f549e4b07e274))


### Features

* added asset references database ([4257873](https://github.com/dre0dru/AddressablesServices/commit/4257873a69faf62d4915093e4b8857c4f934ecd0))
* added single/multiple loaders ([4b3e02a](https://github.com/dre0dru/AddressablesServices/commit/4b3e02a24dac84a354950f00b2de636b1b34ef92))
* added sprite atlases provider ([21fe731](https://github.com/dre0dru/AddressablesServices/commit/21fe731c4541790859d65a27f1b838cd94377271))
* added TMP fonts load ([1d6dd9a](https://github.com/dre0dru/AddressablesServices/commit/1d6dd9a4aa6e83bf2c50d95c2350f52100a40723))
* assets downloader wip ([f1cd059](https://github.com/dre0dru/AddressablesServices/commit/f1cd05983e9500c1a9368954ce1a43e97f138b25))
* assets downloading, project restructure ([58aca9f](https://github.com/dre0dru/AddressablesServices/commit/58aca9f05016cbc54cfaa1c2320f635448a161f7))
* removed multiple assets loader, introduced higher level interface for asset reference loader ([d131385](https://github.com/dre0dru/AddressablesServices/commit/d131385f878e7b0060518152bc3b6294ce402604))
* single/multiple loaders ontroduces ([d6d3dd7](https://github.com/dre0dru/AddressablesServices/commit/d6d3dd7f0c8286671125e25082b259efcba2b21c))


### BREAKING CHANGES

* renamed core interfaces and namespaces
* renamed core interfaces
* changed single loader interface
* changed single loader interface

## [3.0.1](https://github.com/dre0dru/AddressablesServices/compare/v3.0.0...v3.0.1) (2021-08-01)


### Bug Fixes

* wrapped `RequiredMember` attribute in preprocessor directive ([9c22785](https://github.com/dre0dru/AddressablesServices/commit/9c227859da01e00714bc454a3a3d00f29c330764))

# [3.0.0](https://github.com/dre0dru/AddressablesServices/compare/v2.2.0...v3.0.0) (2021-06-30)


### Bug Fixes

* fixed handle leak on asset preload failure ([4df9bf3](https://github.com/dre0dru/AddressablesServices/commit/4df9bf3f2723e28787217f404eaeb5053e45eb6f))
* regenerated guid to avoid conflicts with other packages ([a9ae311](https://github.com/dre0dru/AddressablesServices/commit/a9ae311ce2785c5e3536cf2d5009959c7ea109b0))
* update menu item name ([b398758](https://github.com/dre0dru/AddressablesServices/commit/b39875808f63854fe1d7cca5b8870e26045fa3ab))


### Features

* moved to scripting define utility to optional dependencies ([38e2abe](https://github.com/dre0dru/AddressablesServices/commit/38e2abefc3c20862e5f271b40dd0b3431ee4e13d))


### BREAKING CHANGES

* moved loader classes to different namespace

# [2.2.0](https://github.com/dre0dru/AddressablesServices/compare/v2.1.0...v2.2.0) (2021-04-09)


### Features

* extracted `params` overload for `IAddressablesLoader` to extension methods, added extension methods for assets loading ([e0eb22f](https://github.com/dre0dru/AddressablesServices/commit/e0eb22f05a5f6ebb865ebebef73eec6e4df471d1))

# [2.1.0](https://github.com/dre0dru/AddressablesServices/compare/v2.0.0...v2.1.0) (2021-04-04)


### Features

* added `IsAssetPreloaded` methods to `IAddressablesLoader` ([1470742](https://github.com/dre0dru/AddressablesServices/commit/1470742f8724c1e0702b7d6bc6d13c432790a625))

# [2.0.0](https://github.com/dre0dru/AddressablesServices/compare/v1.1.0...v2.0.0) (2021-03-27)


### Features

* added params[] overload for AssetReference and AssetLabelReference methods ([3e8f721](https://github.com/dre0dru/AddressablesServices/commit/3e8f7210aad49763859522b0f0ea552de7a90dc0))
* changed IAddressablesLoader component extensions to accept only AssetReferenceComponent as its argument ([0bf948c](https://github.com/dre0dru/AddressablesServices/commit/0bf948c4bfacbee51c31b0148a833430a6c9a304))


* chore!: unity version update, packages update ([245ffd8](https://github.com/dre0dru/AddressablesServices/commit/245ffd8825b873a35529eb0751ee1028b0575d90))


### BREAKING CHANGES

* addressables and unitask packages updated to latest versions

# [1.1.0](https://github.com/dre0dru/AddressablesServices/compare/v1.0.1...v1.1.0) (2021-02-27)


### Features

* replaced own scripting define utils with upm packaged ([11b74b3](https://github.com/dre0dru/AddressablesServices/commit/11b74b3560028628b452947bf1f86369c5165cb0))

## [1.0.1](https://github.com/dre0dru/AddressablesServices/compare/v1.0.0...v1.0.1) (2021-02-26)


### Bug Fixes

* changed changelog filepath for semantic release ([0ec806a](https://github.com/dre0dru/AddressablesServices/commit/0ec806a05dbf4c48f7aa07636759e28fdf59b7a1))
