<template>
    <div class="wrap-custome-column" :style="{display : showSetting}">
        <div class="wrap-custome-column-content">
            <div class="custome-row-flex custome-title">
                <div class="title">Tùy chỉnh cột</div>
                <div class="icon-36 button-refresh" @click="refreshHeader()">
                    <div class="icon-20 icon-refresh"></div>
                </div>
            </div>
            <InputSearch/>
            <draggable class="draggable" v-model="headersCustome">
                <transition-group>
                    <div class="wrap-item-column" v-for="(item, index) in headersCustome" :key="index">
                        <div class="wrap-input-checkbox">
                            <div :class="{'input-checkbox-default' : item.Checked == false, 
                            'input-checkbox-checked' : item.Checked == true}" 
                            @click="clickCheckBox(item)">
                            </div>
                            <div class="title-item">{{ item.Caption }}</div>
                        </div>
                        <div class="icon-24 wrap-button-move">
                            <div class="icon-20 icon-button-move"></div>
                        </div>
                    </div>
                </transition-group>
            </draggable>
        </div>
        <div class="draggable-wrap-button">
            <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText" @click.native="clickSave()"/>
        </div>
    </div>
</template>
<script>
import draggable from 'vuedraggable'
import InputSearch from '../../components/base/input-search/BaseInputSearch.vue'
import Button from "../base/button/BaseButton.vue"
export default {
    name: "CustomeColumn",
    
    components: {
            draggable,
            InputSearch,
            Button,
    },
    props:{
        showPopupEditColumn:{
            type: Boolean,
            default(){
                return false;
            }
        },
        headers:{
            type: Array,
            default(){
                return [];
            }
        }
    },
    data() {
        return {
            headersCustome: [],
        }
    },
    created() {
        this.headersCustome = [...this.headers];
    },
    computed: {
        /**
         * @description Thay đổi trạng thái của popup setting "Tùy chỉnh cột"
         * @date 18/09/2021
         * @createdBy LHTDung
         */
        showSetting() {
            if (this.showPopupEditColumn) {
                return "flex";
            } else {
                return "none";
            }
        },
    },
    methods: {
        /**
         * @description Xử lý sự kiện click checkbox
         * @created LHTDung
         * @date 24/09/2021
         */
        clickCheckBox(item){
            item.Checked = !item.Checked;
        },

        /**
         * @description Xử lý sự kiện khi click button Lưu
         * @created LHTDung
         * @date 24/09/2021
         */
        clickSave(){
            this.$emit('updateListHeaderGrid', this.headersCustome);
        },

        /**
         * @description Xử lý sự kiện click button refresh
         * @created LHTDung
         * @date 24/09/2021
         */
        refreshHeader(){
            this.$emit('updateListHeaderDefault');
        }
    },
    watch: {
        headers: function(){
            this.headersCustome = [...this.headers];
        }
    }
}
</script>
<style>
    @import "../../css/layout/custome-column.css";
</style>