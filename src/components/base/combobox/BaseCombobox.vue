<template>
<!------------------------------------------------ COMBOBOX TAG ----------------------------------------------------------->
    <div class="wrap-combobox">
        <div class="dx-field">
            <div class="dx-field-value">
                <DxTagBox
                :items="dataSource"
                :show-selection-controls="true"
                :search-enabled="true"
                :placeholder="placeholder"
                />
            </div>
            <div class="icon-36 wrap-icon-combobox">
                <div class="icon-20 icon-combobox">

                </div>
            </div>
        </div>
    </div>
    
</template>

<script>
import DxTagBox from 'devextreme-vue/tag-box';
export default {
    components: {
        DxTagBox,
    },
    props:{
        placeholder:{
            type: String,
            default(){
                return "";
            }
        },

        //Danh sách chứa tất cả bản ghi ( Vị trí || Nhân viên )
        listObjectData:{
            type: Array,
            deafult(){
                return [];
            }
        },

        //Loại combobox để phân biệt Combobox tag của Vị trí và Nhân viên
        nameCombobox:{
            type: String,
            default(){
                return '';
            }
        }
    },
    data(){
        return {
            dataSource: [],   //Danh sách chứa tên của các vị trí
        }
    },
    created() {
        /**
         * @description Lưu data vào danh sách dataSource
         * @created LHTDung
         * @date 22/09/2021
         */
        if(this.listObjectData){
            if(this.nameCombobox == this.$resourceVn.POSITION){
                this.listObjectData.forEach(item => {
                    this.dataSource.push(item.PositionName);
                });
            }
            if(this.nameCombobox == this.$resourceVn.EMPLOYEE){
                this.listObjectData.forEach(item => {
                    this.dataSource.push(item.FullName);
                });
            }
        }
    },
}
</script>
<style scoped>
    @import "../../../css/base/combobox/base-combobox.css";
</style>