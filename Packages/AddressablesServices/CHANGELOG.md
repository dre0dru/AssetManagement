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
