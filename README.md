## 프로젝트 소개

- 사용 엔진 : 유니티<br>
설명 : 게임이란 형태는 딱히 없고 어드레서블 사용, 리소스 로드, 간단한 조작정도만 있음

## 작업 내용
- 어드레서블 에셋을 활용하여 아마존 S3를 통해 에셋 관리
![Image](https://github.com/user-attachments/assets/eae8d1a9-081d-4cd1-b9f3-96e8968cb2ed)
<br><p align="center">아마존 S3에서 에셋 올림</p><br><br>

![Image](https://github.com/user-attachments/assets/f10a893f-1b24-4616-8e95-c8e4673794f7)
<br><p align="center">아마존 S3에 Remote Path를 통해 다운로드 확인</p><br><br>

![Image](https://github.com/user-attachments/assets/068372c1-887e-46cc-83ba-da8d23cf6521)
![Image](https://github.com/user-attachments/assets/52d7de91-5e1a-42f3-8baa-4d00272c212b)
<br><p align="center">다운로드를 시도하면 비동기로 작동하여 로딩바가 나오고 다운로드가 완료되면 다운로드한 에셋 사용 가능</p><br><br>
