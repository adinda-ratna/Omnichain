// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/utils/structs/EnumerableSet.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract NinjaCatalyst is ERC20 {


  address public owner;
  IERC20  _token;

 
   modifier onlyOwner {
    require(owner == msg.sender); 
    _;
}

    constructor() ERC20("Ninja Catalyst", "NCT") {
      owner = msg.sender; 
       _token = IERC20(address(this));
    }

 
    function mint(uint256 value) 
        public 
        onlyOwner
        returns (bool)
    {
        _mint(msg.sender, value  = value * 10 ** 18);
        return true;
    }

    function GetGameToken() public {
        uint256 _give_= 1 * 10 ** 18;
        require(_give_ <= balanceOf(address(this)), "Low Balance");
        _token.transfer(msg.sender, _give_);
    }

    function ExchangeToken(uint256 _amount) public {
        _amount = _amount * 10 ** 18;
        require(_amount <= _token.balanceOf(msg.sender), "Low Balance");
         transfer(msg.sender, _amount);
    }

     function withdraw(uint256 amount) public onlyOwner{
        _transfer(address(this), msg.sender, amount * 10 ** 18);
    }

    function GetSmartContractBalance() external view returns(uint) {
        return _token.balanceOf(address(this));
    }

    function GetuserBalance(address _account) public view returns(uint256) {
        uint256 Bal = _token.balanceOf(_account);
        return Bal;
    }

    function GetCurrentTime() public view returns(uint256 _result){
      return _result = block.timestamp;
    }
}