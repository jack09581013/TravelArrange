
# 高雄市政府計畫 - 旅遊行程自動安排

## 網站
網站分成三個頁面：
* 景點介紹：提供給使用者綜覽所有景點。
* 自動行程安排：提供給使用者進行行程自動安排，輸入各項參數，包括旅遊時間、對各項景點種類的喜好等。
* 顯示安排結果：將第一步輸入的參數回傳至伺服器，經過C#語言撰寫之MDTRA類別模組運算後，回傳結果至顯示結果頁面並將資料顯示出來。

## 景點介紹功能
* 綜覽所有景點，景點依據分類排序。
* 按下「了解更多」可以看見詳細資料。
* 按下分類文字，可跳至特定分類的景點。

![](https://lh5.googleusercontent.com/wapy9RrxSpzfqERx3fK6LJVVXOtj1gefav63NdKWllNcS3hH7M45rCosFpcQeIMIQF1KVpbvGEB8dQE=w1920-h950-rw)

## 自動行程安排功能
* 按下「安排旅遊日期」，輸入遊玩的開始時間與結束時間。

![](https://lh4.googleusercontent.com/vgBseQX61izBP7-ewfyIDSqvHBb0PIkYXK348Nu0yloAJgRx9sJzZPCRlSUUw7c-mwhVB70Fxjj_4A8=w1920-h950)
* 按下「個人興趣分數」，輸入各分類喜好程度，數值不小於1、不大於10。

![](https://lh5.googleusercontent.com/ZPM44tz6xwc3RouzyyNrx3d80LsSjbj8hGT5i3v9IXILP1f6GZ7O0atb2ozIdZHYOkaxLnYgeMFa0RI=w1920-h950)
* 按下「查看行程」，開始進行行程安排。

![](https://lh6.googleusercontent.com/YVis5xbgW7IIrG2W08KlgNtBU_X_svMZw83Xqr08HINW2LcxuVuWtsgnRCT5XMQB188Sb5L9yZi25l0=w1920-h950)
* 若未完成所有參數設置，網頁將跳出對話框，提醒尚未完成的參數設定

## 顯示安排結果
* 此頁面依據「自動行程安排」參數進行MDTRA行程安排。安排結果需按下右側日期，選擇一天後便可觀看結果。

![](https://lh5.googleusercontent.com/MV5krqz0oRLrYwOdHbb1CrLyHE_V8yx8U7oL9tF0F3q0a78KxdF3erkbbgcgd2f332oolRE-6djIeGA=w1920-h950)
![](https://lh3.googleusercontent.com/coTBBKcigHmz7dpv-H8RNY-4BPO5sYZ4VOS9FQVlMQ-hWxGdUtdb-aXNTVa8ah5P3HKCXjhgxEDpTck=w1920-h950)
* 按下行程列表中的景點名稱，可看其詳細訊息。

![](https://lh4.googleusercontent.com/r7hiWLuscdzQ7hptHLiKmoWVINqRhc5OgQVO-_TDowDAMs0c6IbOxKPPpCvcfAY-lHOnDMNwT-2vctM=w1920-h950-rw)

