<template>
    <div class="dx-field-value">
        <DxDropDownBox
          ref="dropdownBox"
          :value="value"
          :show-clear-button="false"
          :data-source="treeDataSource"
          value-expr="DepartmentId"
          display-expr="DepartmentName"
          @value-changed="syncTreeViewSelection($event)"
        >
  
          <!-- <div slot="fieldTemplate" slot-scope="{ data }">
                <div class="custom-item">
                    <div class="product-name">
                        <DxTextBox
                            :value="data"
                            :read-only="true">
                            {{ data }}
                        </DxTextBox>
                    </div>
                </div>
            </div> -->
          <template #content="{  }">
            <DxTreeView
              :ref="treeViewRefName"
              :value="value"
              :data-source="treeDataSource"
              :select-by-click="true"
              :searchEnabled="true"
              placeholder="Tìm kiếm"
              noDataText="Không có dữ liệu"
              data-structure="plain"
              key-expr="DepartmentId"
              parent-id-expr="ParentId"
              root-value="00000000-0000-0000-0000-000000000000"
              selection-mode="multiple"
              show-check-boxes-mode="normal"
              display-expr="DepartmentName"
              @content-ready="$event.component.selectItem(treeBoxValue)"
              @item-selection-changed="treeView_itemSelectionChanged($event)"
            />
          </template>
        </DxDropDownBox>
    </div>
</template>
<script>
// import treeData from "../../../js/fake-data/data-dropdown-single"
// import DxTextBox from "devextreme-vue/text-box";
import DxDropDownBox from 'devextreme-vue/drop-down-box';
import DxTreeView from 'devextreme-vue/tree-view';
export default {
    name: "DropdownMultiple",
    components: {
        DxDropDownBox,
        DxTreeView,
        // DxTextBox
    },
    props:{
        treeDataSource:{
          type: Array,
          default(){
            return [];
          }
        },

        //Dữ liệu Đơn vị áp dụng của bản ghi (xem chi tiết/sửa)
        value:{
          type: String,
          default(){
            return '';
          }
        }
    },
    data() {
        return {
            treeBoxValue: this.value,
            treeViewRefName: 'tree-view',
        };
    },
    
    methods: {
        /**
         * @Description Xử lý sự kiện khi item được click và bind dữ liệu
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        syncTreeViewSelection() {
          if (!this.$refs[this.textBoxRefName]) return;
          if (!this.treeBoxValue) {
            this.$refs[this.textBoxRefName].instance.unselectAll();
          } else {
            this.$refs[this.textBoxRefName].instance.selectItem(this.treeBoxValue);
          }
        },

        /**
         * @Description Xử lý sự kiện cho item được trọn trong tree data
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        treeView_itemSelectionChanged(e) {
          this.treeBoxValue = e.component.getSelectedNodeKeys();
        },
    },
}
</script>
<style scoped>
    @import "../../../css/base/dropdown-multiple/base-dropdown-multiple.css";
</style>