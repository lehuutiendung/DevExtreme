<template>
    <div class="header-wrap">
        <div class="header-left">
            <div class="wrap-button-ninedot">
                <div class="svg-icon header-icon ninedot"></div>
            </div>
            <div class="header-text company-name">
                <div class="icon-32 logo-app"></div>
                <div class="logo-text">Tiền lương</div>
            </div>
            <div class="svg-icon arrow-select"></div>
        </div>
        <div class="header-right">
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text">Tổng quan</div>
                    <div class="border-active"></div>
                </div>
            </div>
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text">Thành phần lương</div>
                    <div class="border-active"></div>
                </div>
            </div>
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text active-item-header">Chính sách</div>
                    <div class="border-active actived"></div>
                </div>
                <div class="svg-icon icon-20 arrow-icon"></div>
            </div>
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text">Dữ liệu tính lương</div>
                    <div class="border-active"></div>
                </div>
                <div class="svg-icon icon-20 arrow-icon"></div>
            </div>
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text">Tính lương</div>
                    <div class="border-active"></div>
                </div>
                <div class="svg-icon icon-20 arrow-icon"></div>
            </div>
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text">Chi trả lương</div>
                    <div class="border-active"></div>
                </div>
            </div>
            <div class="router-item-header">
                <div class="wrap-item">
                    <div class="item-text">Báo cáo</div>
                    <div class="border-active"></div>
                </div>
                <div class="svg-icon icon-20 arrow-icon"></div>
            </div>
        </div>
        <div class="header-info-account">
            <div class="wrap-info">
                <div class="wrap-icon-24">
                    <div class="svg-icon icon-24 setting"></div>    
                </div>
                <div class="wrap-icon-24">
                    <div class="svg-icon icon-24 noti"></div>    
                </div>
                <div class="wrap-icon-24">
                    <div class="svg-icon icon-24 help"></div>    
                </div>
                <div class="wrap-icon-24">
                    <div class="svg-icon icon-24 three-dot-circle"></div>    
                </div>
                <div class="icon-32 avatar-info"></div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: "TheHeader",
}
</script>
<style scoped>
    @import "../../css/layout/header.css";
</style>