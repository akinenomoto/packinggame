# Packing Game

箱に荷物を詰め、最後にトラックへ積み込む2段階のパズルゲームです。

## ゲーム概要

ゲームは次の2つのフェーズで進行します。

1. **梱包フェーズ**
   荷物をドラッグして箱のグリッドへ配置し、すべての箱を閉じます。
2. **積み込みフェーズ**
   梱包した荷物をトラックの荷台へ配置します。すべての荷物を積み込むとクリアです。

## 開発環境

- Unity `6000.6.5f1`
- Universal Render Pipeline
- Input System
- 対応プラットフォーム: Unity Editorで設定したビルド対象に準拠

## 起動方法

1. Unity HubでUnity `6000.6.5f1`をインストールします。
2. Unity Hubから本リポジトリのフォルダーをプロジェクトとして開きます。
3. `Assets/Scenes/PackingScene.unity`を開きます。
4. Unity Editor上部の **Play** ボタンを押します。

ビルドして実行する場合は、**File > Build Profiles**（または **File > Build Settings**）で次のシーンが登録されていることを確認してください。

1. `Assets/Scenes/PackingScene.unity`
2. `Assets/Scenes/TruckScene.unity`

`PackingScene`を開始シーンにすると、梱包完了後に自動的に`TruckScene`へ遷移します。

## 操作方法

### 梱包フェーズ

| 操作 | 動作 |
| --- | --- |
| 左クリック + ドラッグ | 荷物を移動 |
| `R` | 荷物を90度回転 |
| 左クリックを離す | 箱のグリッドへ配置 |

箱に配置できない位置でマウスボタンを離すと、荷物は元の位置へ戻ります。

### 積み込みフェーズ

| 操作 | 動作 |
| --- | --- |
| 荷物を左クリック | 荷物を選択 |
| `R` | 選択中の荷物をY軸回転 |
| 床を左クリック | 選択中の荷物をトラックへ配置 |

配置できない位置や、満杯の列には荷物を配置できません。

## プロジェクト構成

```text
Assets/
├── Scenes/
│   ├── PackingScene.unity   # 2D梱包フェーズ
│   └── TruckScene.unity     # 3D積み込みフェーズ
└── Scripts/
    ├── Common/              # ゲーム進行、UI、タイマーなど
    ├── Phase2D/             # 箱と荷物の配置ロジック
    └── Phase3D/             # トラックへの積み込みロジック
```

主なスクリプトは次のとおりです。

- `GameManager`: フェーズ管理とシーン遷移
- `RoomManager`: 梱包フェーズの進行管理
- `Grid2DManager` / `Placement2D`: 箱への2D配置
- `TruckManager`: トラック上の荷物管理
- `Grid3DManager`: トラック内の3Dグリッド管理

## 開発時の注意

- `Library/`、`Temp/`、`Logs/`などのUnity生成フォルダーはGit管理対象外です。初回起動時にUnityが自動生成します。
- シーンを追加・変更した場合は、Build Profiles（Build Settings）のシーン一覧も更新してください。
- 既定のステージ構成や配置は各シーンのInspectorから調整できます。

## ライセンス

ライセンス情報は未設定です。利用・再配布条件を定める場合は、ライセンスファイルを追加してください。