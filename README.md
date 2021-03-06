highgui_pos_cleaner
====
OpenCVのhighguiで表示したウインドウの位置を全部クリアするプログラム。

使い方
----
highgui_pos_cleaner.exeを実行するだけ。


メモ
----
Windows環境でcv::imshow()を使ってウインドウを表示した場合、
次回プログラム実行時にも同じ位置にウインドウが表示される。
これはcv::imshow()を使って表示したウインドウのサイズ・位置がレジストリに保存されていて、
次回起動時にウインドウの状態を復元するためである。

* https://github.com/Itseez/opencv/blob/master/modules/highgui/src/window_w32.cpp#L303

この機能は、実験などで頻繁にプログラムを起動する場合に非常に便利な反面、
OSのデスクトップサイズを変更した場合に不具合が生じる。

例えば、普段デュアルディスプレイ環境で開発を行っていて、
その開発環境をデモなどで持ち出してシングルディスプレイ環境で実行した場合、
cv:imshow()で表示されるウインドウがデスクトップの外に表示されてしまって見えなくなり、
非常に焦ることがある。(※体験談)

通常のウインドウの場合、デスクトップの外に表示されてしまったウインドウでも、
次の手順で操作を行うと、デスクトップの中にウインドウ位置を戻すことができる。

  - 1. タスクバーを使って目的のウインドウをアクティブにする
  - 2. alt + スペースキーを押す (ウインドウメニューが表示される)
  - 3. Mキーを押す (メニューからウインドウの移動が選ばれる)
  - 4. カーソルキーを使ってデスクトップの中にウインドウを移動させる

ただし、この方法を使うことができるのは、alt+スペースキーを押したときに
ウインドウメニューが表示されるウインドウに限られる。
cv::imshow()で表示されるウインドウは
なぜかalt+スペースキーの組み合わせが効かないため、
この方法を使ってウインドウを動かすことができない。

そこで、highgui_pos_cleaner.exeではウインドウのサイズ・位置が保存されている
レジストリの以下の場所を全部消すことで、
cv::imshow()で表示されるウインドウの状態をすべてクリアしている。

* HKEY_CURRENT_USER\Software\OpenCV\HighGUI\Windows\

ウインドウがデスクトップの外に表示されてしまう問題に対しては、
highguiにはcv::moveWindow()という関数も用意されているので、
あらかじめ以下のような実装を行っておくことで
非常時にウインドウ位置をリセットする方法も考えられる。

    while(true) {
      capture >> capture_img;
      cv::imshow("captureimage_window", capture_img);

      int c = cv::waitKey(1);
      if (c == 27) {
        break;
      }
      else if (c == 'r') {
      	// reset window position
      	cv::moveWindow("captureimage_window", 0, 0);
      }
    }

ちなみに…
----
regコマンドを使ってレジストリを削除する方法もあります。

    reg.exe delete HKCU\Software\OpenCV\HighGUI\Windows\ /f

Reg delete (Microsoft TechNet)
* https://technet.microsoft.com/en-us/library/cc742145.aspx