import { task } from "hardhat/config";
import { HardhatRuntimeEnvironment } from "hardhat/types";

const NinjaCatalyst = "NinjaCatalyst";
const NinjaCatadi = "NinjaCatadi";

const main = async (args: any, hre: HardhatRuntimeEnvironment) => {
  if (hre.network.name !== "athens") {
    throw new Error(
      '🚨 Please use the "athens" network to deploy to ZetaChain.'
    );
  }

  const [signer] = await hre.ethers.getSigners();
  console.log(`🔑 Using account: ${signer.address}\n`);

  const NinjaCatalystContract = await hre.ethers.getContractFactory(NinjaCatalyst);
  const ninjaCatalystContract = await NinjaCatalystContract.deploy();
  await ninjaCatalystContract.deployed();

  const NinjaCatadiContract = await hre.ethers.getContractFactory(NinjaCatadi);
  const ninjaCatadiContract = await NinjaCatadiContract.deploy();
  await ninjaCatadiContract.deployed();


  console.log(`🚀 Successfully deployed contract on ZetaChain.
  📜 NinjaCatalyst Contract address: ${ninjaCatalystContract.address}
  🌍 Explorer: https://explorer.zetachain.com/address/${ninjaCatalystContract.address}
  `);

  console.log(`🚀 Successfully deployed contract on ZetaChain.
  📜 NinjaCatadi Contract address: ${ninjaCatadiContract.address}
  🌍 Explorer: https://explorer.zetachain.com/address/${ninjaCatadiContract.address}
  `);

};

task("deploy", "Deploy the contract").setAction(main);