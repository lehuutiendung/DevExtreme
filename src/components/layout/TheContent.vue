<template>
    <div class="content-wrap">
        <div class="content-area">
            <div class="content-title">
                <div class="title-row">
                    <div class="title">Chính sách lương</div>
                    <ButtonIcon :buttonName="this.$resourceVn.ButtonIconAddText"/>
                </div>
            </div>
            <div class="content-body"> 
                <div class="content-body-background">
                    <div class="content-body-tool">
                        <div class="content-body-tool-left">
                           <div class="div-input-search">
                                <div class="wrap-icon-input-search">
                                    <div class="icon-20 icon-input-search"></div>
                                </div>
                                <input class="input-search" type="text" placeholder="Tìm kiếm" >
                            </div>
                        </div>
                        <div class="content-body-tool-right">
                            
                        </div>
                    </div>
                </div>   
            </div>
            <div class="content-grid">
                <BaseDataGrid/>
            </div>
            <TheFooter/>
        </div>
    </div>
</template>
<script>
import TheFooter from "../layout/TheFooter.vue"
import ButtonIcon from "../base/BaseButtonIcon.vue"
import BaseDataGrid from "../base/BaseDataGrid.vue"
export default {
    name: "TheContent",
    components: {
        TheFooter,
        ButtonIcon,
        BaseDataGrid
    },
        
}
</script>
<style scoped>
    @import "../../css/layout/content.css";
</style>