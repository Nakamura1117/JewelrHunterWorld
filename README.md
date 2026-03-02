# JuwelyHunterWorld

## はじめに

- このゲームはUnityを使用した2Dゲームの実装を学ぶために作成されているものです。
- ゲームとして、スタートからゲームクリアまで遊べるように作りました。

## 作品の概要

- ２Ｄスクロールアクションです。  
  マップから各ステージへ入り、最後にボスと戦うゲームになります。

### デモプレイ
https://github.com/user-attachments/assets/5dff3bc6-2c88-4aa6-824f-4f521c4143c0

#### 操作方法

ステージ

- 移動・・・「←」キー、「→」キー
- ジャンプ・・・Spaceキー
- 攻撃（矢を放つ）・・・Enterキー、Ｆキー、Ｚキー

ワールドマップ

- 移動・・・「↑」キー、「↓」キー、「←」キー、「→」キー
- ステージに入る・・・Enterキー

#### ギミック

| 参考画像 |  ギミック  | 説明                                                                                               |
| :------- | :--------: | :------------------------------------------------------------------------------------------------- |
|<img width="170" height="95" alt="attack" src="https://github.com/user-attachments/assets/35469e18-3b04-46f1-bfe0-fb2d76717165" />|    攻撃    | 矢を放ちます。画面左上に表示されている残弾が０になると、打てません。                               |
|<img width="347" height="43" alt="hp" src="https://github.com/user-attachments/assets/c4d566ce-d094-4485-9923-c48231963934" />|  ダメージ  | プレイヤーが敵や砲弾にあたるとダメージを受けます。プレイヤーのＨＰが０になるとゲームオーバーです。 |
|<img width="125" height="133" alt="stage_entrance" src="https://github.com/user-attachments/assets/19c3cace-367f-4227-9f87-d63568eca737" /> |  ステージ  | Enterキーで入場できます。鍵を使用しないと入れない場合も。                                          |
| <img width="121" height="116" alt="goal" src="https://github.com/user-attachments/assets/c1ab02cb-de30-400e-9f04-d6a14b3f950a" /> | ゴール看板 | ステージクリアの目印です。クリアすると踊りだします。                                               |
|<img width="102" height="73" alt="enemy" src="https://github.com/user-attachments/assets/d021cdc8-71a0-4d5a-9478-49f594975a0f" />|     敵     | ハリネズミ。あたると痛いと思われます。                                                             |
|<img width="246" height="249" alt="boss" src="https://github.com/user-attachments/assets/8c988716-d482-4b0f-9208-64ce14dd4eb4" />|    ボス    | 倒すとゲームクリア。攻撃は当然痛い。                                                               |
|<img width="229" height="117" alt="canon" src="https://github.com/user-attachments/assets/b5b7391f-960c-4b9c-9a4a-9a886bd10761" />|    大砲    | 砲弾を発射してきます。砲弾にあたると痛い。                                                         |
|<img width="276" height="61" alt="needle" src="https://github.com/user-attachments/assets/45402d41-f726-4761-aece-05549a668a4f" />|     針     | 刺さったらゲームオーバー。最初からやり直しです。                                                   |
|<img width="64" height="69" alt="itembox" src="https://github.com/user-attachments/assets/2c16337f-dd5a-4603-bbed-992cee88e19e" />|    宝箱    | アイテムボックス。「矢」「回復」「鍵」のいずれかが入手できます。                                   |
|          |  スイッチ  | 移動床を起動するスイッチです。                                                                     |
|<img width="238" height="84" alt="score" src="https://github.com/user-attachments/assets/507573b5-6895-4f81-9d1e-71c8199d6267" />|   スコア   | ステージに配置されている色つきのアイテムに触れることで取得できます。色によって点数が変わります。   |

## プログラムの場所

- Assets/Scripts・・・各スクリプト　

## 使用技術・ツール

- 使用言語:C#
- 開発ツール:Unity Editor 6000.3.5f2
  　　　　　 Microsoft Visual Studio Community 2026（18.3.1）

## 参考書籍・使用アセット

- 参考書籍　楽しい2Dゲームの作り方 Unity6ではじめるゲーム開発入門 第三版（STUDIO SHIN様著）  
  　ＵＲＬ：https://www.shoeisha.co.jp/book/detail/9784798192604
- 使用アセット　SunnyLand Artwork（Ansimuz様作）  
  　ＵＲＬ：https://assetstore.unity.com/packages/2d/characters/sunny-land-103349
- 効果音　効果音工房 様
  　ＵＲＬ：https://umipla.com/

## 製作期間

- 1か月程度

## 制作のポイント

## 課題

- ステージ数、敵の種類、ギミックが少ないコンテンツの少なさ
- 同じステージのスコアが重複して加算される
- スクリプトの保管場所が整理されていない

## おわりに

- 当初の目的であった「Unityでの2Dゲームの実装を学ぶ」ということについて、
  最低限形にすることができたと思います。
- フォルダ整理やプログラム構造に見直ししたい点も多数ありました。
  時間があれば直していきたいです。
- セーブや外部ファイルがあまり触れられなかったので、今後勉強したいです。
