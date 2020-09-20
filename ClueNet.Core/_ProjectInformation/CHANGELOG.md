### todo

* 訊號對齊
  * `Vector Clock` 屬性, 保證先後順序
* 支援多張多種擷取卡/硬體卡	(完成)
  * 新增類別與功能, 用Plug-In載入
* 訊號群組設定
  * 透過設定檔
* 輔助挑選重要參數
  * 標記變化劇烈的訊號(高低差)
* 分享擷取的資訊給外部系統
  * CSV、Socket 通訊、MQTT
* 以 FEMCO 專案為目標, 使用此框架如何導入?
  * 元件訊號的繪圖
  * 訊號繪圖
  * 控制器數值
  * Log 訊息
* 工作日誌
  * 將 SignalState 移動到 IDigitalInputDevice 介面內


### 1.0.0.0 - 2020-??-??

* removals
  * N/A
* bug fixes
  * N/A
* enhancements
  * 實作 IDaqDevice 介面
  * 實作 IDigitalInputDevice 介面